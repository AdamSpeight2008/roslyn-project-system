' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.VisualStudio.Shell.Interop
Imports Microsoft.VSDesigner

Namespace Microsoft.VisualStudio.Editors.SettingsDesigner
    ''' <summary>
    ''' Wrapper class for the settings type resolver that caches previously resolved
    ''' type names.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class SettingsTypeCache
        Implements IDisposable

        Private _multiTargetService As MultiTargetService
        Private _typeResolutionService As System.ComponentModel.Design.ITypeResolutionService
        Private _caseSensitive As Boolean

        ' The list of types that we always know how to find. 
        Private ReadOnly _wellKnownTypes() As Type = {
            GetType(Boolean), GetType(Byte), GetType(Char), GetType(DateTime), GetType(Decimal), GetType(Double),
            GetType(Guid), GetType(Short), GetType(Integer), GetType(Long), GetType(SByte), GetType(Single),
            GetType(TimeSpan), GetType(UShort), GetType(UInteger), GetType(ULong),
            GetType(Drawing.Color), GetType(Drawing.Font), GetType(Drawing.Point), GetType(Drawing.Size),
            GetType(String), GetType(Specialized.StringCollection)
                                                     }

        ''' <summary>
        ''' Given a "normal" ITypeResolutionService, create an instance of the settingstypecache
        ''' </summary>
        ''' <param name="typeResolutionService"></param>
        ''' <remarks></remarks>
        Public Sub New(
                        vsHierarchy As IVsHierarchy,
                        ItemId As UInteger,
                        typeResolutionService As ComponentModel.Design.ITypeResolutionService,
                        caseSensitive As Boolean
                      )
            If typeResolutionService Is Nothing OrElse vsHierarchy Is Nothing Then
                Debug.Fail("We really need a type resolution service or IVsHierarchy in order to do anything interesting!")
                Throw New ArgumentNullException()
            End If

            _multiTargetService = New MultiTargetService(vsHierarchy, ItemId, False)
            _typeResolutionService = typeResolutionService
            _caseSensitive = caseSensitive
        End Sub

        Private Sub Dispose(disposing As Boolean)
            If disposing Then
                If _multiTargetService IsNot Nothing Then
                    _multiTargetService.Dispose()
                    _multiTargetService = Nothing
                End If
                _typeResolutionService = Nothing
            End If
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Get the type corresponding to the given type name. If the type was found in the cache, but
        ''' was obsolete:d (the assembly containing the type has changed) it is removed from the cache.
        ''' 
        ''' The cache is updated with the returned type to improve performance of subsequent resolves...
        ''' </summary>
        ''' <param name="typeName"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSettingType(typeName As String) As System.Type
            ' First, check our list of well known types...
            For Each wellKnownType As System.Type In Me.GetWellKnownTypes()
                If String.Equals(wellKnownType.FullName, typeName) Then Return wellKnownType
            Next
            Return ResolveType(typeName, _caseSensitive)
        End Function


        ''' <summary>
        ''' Get the list of "well known" types (i.e. types that we don't need any type resolution service
        ''' in order to resolve...
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetWellKnownTypes() As System.Type()
            Return _multiTargetService.GetSupportedTypes(_wellKnownTypes)
        End Function

        ''' <summary>
        ''' Is the given type a well known type?
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsWellKnownType(type As System.Type) As Boolean
            For Each wellKnownType As System.Type In GetWellKnownTypes()
                If wellKnownType Is type Then Return True
            Next
            Return False
        End Function

        ''' <summary>
        ''' Private implementation that adds logic to understand our own "virtual" connection string
        ''' and web service URL types
        ''' </summary>
        ''' <param name="persistedSettingTypeName"></param>
        ''' <param name="caseSensitive"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ResolveType(persistedSettingTypeName As String, caseSensitive As Boolean) As System.Type
            Dim t As Type = Nothing

            If String.Equals(persistedSettingTypeName, SettingsSerializer.CultureInvariantVirtualTypeNameConnectionString, StringComparison.Ordinal) Then
                Return GetType(VSDesigner.VSDesignerPackage.SerializableConnectionString)
            ElseIf String.Equals(persistedSettingTypeName, SettingsSerializer.CultureInvariantVirtualTypeNameWebReference, StringComparison.Ordinal) Then
                t = GetType(String)
            Else
                t = _typeResolutionService.GetType(persistedSettingTypeName, False, Not caseSensitive)
            End If

            If t Is Nothing Then Return t
            Return _multiTargetService.GetSupportedType(t, False)
        End Function

        Public Function TypeTransformer(sourceTypeName As String) As String
            Dim qualifiedAssemblyName As String = Nothing

            If Not String.IsNullOrEmpty(sourceTypeName) Then
                Dim t As System.Type = _typeResolutionService.GetType(sourceTypeName, False, Not _caseSensitive)
                If t IsNot Nothing Then
                    qualifiedAssemblyName = _multiTargetService.TypeNameConverter(t)
                End If
            End If

            Return qualifiedAssemblyName
        End Function

    End Class

End Namespace