﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.VisualStudio.Shell.Interop

Namespace Microsoft.VisualStudio.Editors.DesignerFramework

    ''' <summary>
    ''' Implementation of the System.ComponentModel.Design.ITypeDiscoveryService
    ''' 
    ''' The DesignerTypeDiscoveryService differs from the "normal" VS Type Discovery service
    ''' in that it filters out types defined in the current project
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class DesignerTypeDiscoveryService
        Implements System.ComponentModel.Design.ITypeDiscoveryService

        Private _serviceProvider As IServiceProvider
        Private _hierarchy As IVsHierarchy

        ''' <summary>
        ''' Create a new type discovery service associated with the given hierarchy
        ''' </summary>
        ''' <param name="sp"></param>
        ''' <param name="hierarchy"></param>
        ''' <remarks></remarks>
        Public Sub New(
                        sp As IServiceProvider,
                        hierarchy As IVsHierarchy
                      )
            If sp Is Nothing Then Throw New ArgumentNullException(NameOf(sp))
            If hierarchy Is Nothing Then Throw New ArgumentNullException(NameOf(hierarchy))

            _serviceProvider = sp
            _hierarchy = hierarchy

        End Sub

#Region "ITypeDiscoveryService implementation"

        Private Function GetTypes(
                                   baseType As System.Type,
                                   excludeGlobalTypes As Boolean
                                 ) As System.Collections.ICollection Implements System.ComponentModel.Design.ITypeDiscoveryService.GetTypes
            Return GetReferencedTypes(baseType, excludeGlobalTypes)
        End Function

#End Region

        ''' <summary> Get all known types, excluding types in the current project. </summary>

        Public Overridable Function GetReferencedTypes() As ICollection(Of System.Type)
            Return GetReferencedTypes(GetType(Object), False)
        End Function

        ''' <summary> Get an enumeration of all types that we know about in this project. </summary>
        ''' <param name="baseType">Only return types inheriting from this class</param>
        ''' <param name="shouldExcludeGlobalTypes">Exclude types in the GAC?</param>

        Public Overridable Function GetReferencedTypes(
                                                        baseType As System.Type,
                                                        shouldExcludeGlobalTypes As Boolean
                                                      ) As List(Of System.Type)

            Dim dynamicTypeService = DirectCast(_serviceProvider.GetService(GetType(Microsoft.VisualStudio.Shell.Design.DynamicTypeService)), Microsoft.VisualStudio.Shell.Design.DynamicTypeService)
            Dim trs As System.ComponentModel.Design.ITypeResolutionService = Nothing
            Dim tds As System.ComponentModel.Design.ITypeDiscoveryService = Nothing

            If dynamicTypeService IsNot Nothing Then
                tds = dynamicTypeService.GetTypeDiscoveryService(Me._hierarchy, VSITEMID.ROOT)
                trs = dynamicTypeService.GetTypeResolutionService(Me._hierarchy, VSITEMID.ROOT)
            End If

            Dim result As New List(Of System.Type)

            If tds IsNot Nothing AndAlso trs IsNot Nothing Then
                Dim excludedAssemblies As New Dictionary(Of System.Reflection.Assembly, Object)

                Dim outputs() As String = GetProjectOutputs(_serviceProvider, _hierarchy)
                For Each output As String In outputs
                    ' We don't want to return types defined in this project's output because that may include
                    ' the data types generated by this proxy... 
                    Dim assemblyToExclude As System.Reflection.Assembly = AssemblyFromProjectOutput(trs, output)
                    If assemblyToExclude IsNot Nothing Then excludedAssemblies(assemblyToExclude) = Nothing
                Next

                For Each t As System.Type In tds.GetTypes(baseType, shouldExcludeGlobalTypes)
                    If Not excludedAssemblies.ContainsKey(t.Assembly) Then result.Add(t)
                Next
            End If

            Return result
        End Function

        ''' <summary> Get access to the built assemblies for this project. </summary>
        ''' <param name="provider"/>
        ''' <param name="hierarchy"/>
        Protected Overridable Function GetProjectOutputs(
                                                          provider As IServiceProvider,
                                                          hierarchy As IVsHierarchy
                                                        ) As String()
            Try
                Dim buildManager As Microsoft.VisualStudio.Shell.Interop.IVsSolutionBuildManager = TryCast(provider.GetService(GetType(IVsSolutionBuildManager)), IVsSolutionBuildManager)
                If buildManager Is Nothing Then Return New String() {}

                Dim activeConfig(0) As IVsProjectCfg
                Dim activeConfig2 As IVsProjectCfg2

                VSErrorHandler.ThrowOnFailure(buildManager.FindActiveProjectCfg(IntPtr.Zero, IntPtr.Zero, hierarchy, activeConfig))

                activeConfig2 = TryCast(activeConfig(0), IVsProjectCfg2)

                If activeConfig2 IsNot Nothing Then
                    Dim outputgroup As IVsOutputGroup = Nothing
                    activeConfig2.OpenOutputGroup(Microsoft.VisualStudio.Shell.Interop.BuildOutputGroup.Built, outputgroup)
                    Dim outputgroup2 As IVsOutputGroup2 = TryCast(outputgroup, IVsOutputGroup2)
                    If outputgroup2 IsNot Nothing Then
                        Dim output As IVsOutput2 = Nothing
                        VSErrorHandler.ThrowOnFailure(outputgroup2.get_KeyOutputObject(output))
                        If output IsNot Nothing Then
                            Dim url As String = Nothing
                            VSErrorHandler.ThrowOnFailure(output.get_DeploySourceURL(url))

                            If url <> "" Then Return {GetLocalPathUnescaped(url)}
                        End If
                    End If
                End If
            Catch ex As System.Runtime.InteropServices.COMException
                ' We failed to get the project output paths... 
            End Try

            Return New String() {}
        End Function

        ''' <summary>
        ''' Given an assembly path
        ''' </summary>
        ''' <param name="typeResolutionService"></param>
        ''' <param name="projectOutput"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overridable Function AssemblyFromProjectOutput(
                                                                  typeResolutionService As System.ComponentModel.Design.ITypeResolutionService,
                                                                  projectOutput As String
                                                                ) As System.Reflection.Assembly
            If typeResolutionService Is Nothing Then Throw New ArgumentNullException(NameOf(typeResolutionService))

            If typeResolutionService IsNot Nothing Then
                Try
                    If System.IO.File.Exists(projectOutput) Then
                        Dim an As System.Reflection.AssemblyName = System.Reflection.AssemblyName.GetAssemblyName(projectOutput)
                        Dim a As System.Reflection.Assembly = typeResolutionService.GetAssembly(an)
                        Return a
                    End If
                Catch ex As System.IO.FileNotFoundException
                    ' The assembly doesn't exist - it may not have been built yet
                Catch ex As System.IO.IOException
                    ' Unknown error when trying to load the file...
                Catch ex As System.Security.SecurityException
                    ' We didn't have permissions to load the file...
                End Try
            Else
                System.Diagnostics.Debug.Fail("Huh!?")
            End If
            Return Nothing
        End Function


        ''' <devdoc>
        ''' This method takes a file URL and converts it to a local path.  The trick here is that
        ''' if there is a '#' in the path, everything after this is treated as a fragment.  So
        ''' we need to append the fragment to the end of the path.
        ''' </devdoc>
        Protected Shared Function GetLocalPath(
                                                fileName As String
                                              ) As String
            System.Diagnostics.Debug.Assert(fileName IsNot Nothing AndAlso fileName.Length > 0, "Cannot get local path, " & NameOf(fileName) & " is not valid")

            Dim uri As New Uri(fileName)
            Return uri.LocalPath & uri.Fragment
        End Function

        ' VSWhidbey 460000
        ' VSCore does not properly escape paths with the certain characters (for example, #) in 
        ' the URIs they provide.  Instead, if the path starts with file:', we will assume
        ' the rest of the string is the non-escaped path.
        Protected Shared Function GetLocalPathUnescaped(
                                                         url As String
                                                       ) As String
            Dim filePrefix As String = "file:///"
            If url.StartsWith(filePrefix, StringComparison.OrdinalIgnoreCase) Then Return url.Substring(filePrefix.Length)
            Return GetLocalPath(url)
        End Function

    End Class

End Namespace
