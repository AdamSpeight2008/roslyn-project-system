﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports System.Windows.Forms

Namespace Microsoft.VisualStudio.Editors.AddImports
    Friend Module Util
        Public Function ProcessMnemonicString(input As String) As Nullable(Of Char)
            Dim mnemonicChar As Nullable(Of Char) = Nothing
            Dim i As Integer = 0

            While i < input.Length
                If (input(i) = "&"c) AndAlso (i + 1 < input.Length) Then
                    If (input(i + 1) <> "&"c) Then Exit While
                    i += 2
                    Continue While
                End If
                i += 1
            End While

            If (i < input.Length - 1) Then
                mnemonicChar = input(i + 1)

                Dim first As String = String.Empty
                If (i > 0) Then first = input.Substring(0, i)

                Dim second As String = String.Empty
                If (i < input.Length - 2) Then second = input.Substring(i + 2, input.Length - i - 2)
            End If

            Return mnemonicChar
        End Function

        Public Function NextControl(c As Control) As Control
            Return CType(c.Tag, ControlNavigationInfo).NextControl
        End Function

        Public Function PreviousControl(c As Control) As Control
            Return CType(c.Tag, ControlNavigationInfo).PreviousControl
        End Function

        Public Sub SetNavigationInfo(c As Control, nextControl As Control, previousControl As Control)
            c.Tag = New ControlNavigationInfo(nextControl, previousControl)
        End Sub
    End Module
End Namespace