' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Option Strict On
Option Explicit On
Imports Microsoft.VisualStudio.Shell.Interop

Namespace Microsoft.VisualStudio.Editors.Common

    ''' <summary> Wrapping IVsStatusbar to update IDE status bar. </summary>
    Friend Class VsStatusBarWrapper

        Public Sub New(vsStatusBar As IVsStatusbar)
            Debug.Assert(vsStatusBar IsNot Nothing, "Must provide " & NameOf(IVsStatusbar) & "!")

            _vsStatusBar = vsStatusBar
        End Sub

        Public Sub StartProgress(
                                  label As String,
                                  total As Integer
                                )
            Debug.Assert(total > 0, NameOf(total) & " must > 0!")
            _vsStatusBarCookie = 0
            _completed = 0
            _total = total
            _vsStatusBar.Progress(_vsStatusBarCookie, 1, label, CUInt(_completed), CUInt(_total))
        End Sub

        Public Sub UpdateProgress(
                                   label As String
                                 )
            Debug.Assert(_vsStatusBarCookie > 0, "Haven't " & NameOf(StartProgress) & "!")
            If _vsStatusBarCookie = 0 Then Exit Sub

            _completed += 1
            Debug.Assert(_completed <= _total)
            If _completed <= _total Then _vsStatusBar.Progress(_vsStatusBarCookie, 1, label, CUInt(_completed), CUInt(_total))
        End Sub

        Public Sub StopProgress(
                                 label As String
                               )
            Debug.Assert(_vsStatusBarCookie > 0, "Haven't " & NameOf(StartProgress) & "!")
            If _vsStatusBarCookie = 0 Then Exit Sub
            _vsStatusBar.Progress(_vsStatusBarCookie, 0, label, CUInt(_total), CUInt(_total))
        End Sub

        Public Sub SetText(
                            text As String
                          )
            _vsStatusBar.SetText(text)
        End Sub

        Private _vsStatusBar As IVsStatusbar

        Private _vsStatusBarCookie As UInteger = 0
        Private _total As Integer = 0
        Private _completed As Integer

    End Class
End Namespace
