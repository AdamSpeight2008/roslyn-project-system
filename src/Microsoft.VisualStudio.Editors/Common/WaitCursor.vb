' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Option Strict On
Option Explicit On
Imports System.Windows.Forms

Namespace Microsoft.VisualStudio.Editors.Common

    ''' <summary> Wait Cursor. </summary>
    ''' <remarks>
    ''' Utility class that will display a wait cursor over
    ''' the lifetime of the object.   It is designed to be used
    ''' with the Using keyword as follows:
    ''' <code>
    ''' Sub Func()
    ''' Using New WaitCursor
    ''' '''do work
    ''' End Using
    ''' End Sub
    ''' </code>
    ''' </remarks>
    Friend Class WaitCursor
        Implements IDisposable

        Private _previousCursor As Cursor

        ''' <summary> Constructor. </summary>
        ''' <remarks> Changes the cursor to a wait cursor, until the class is Disposed.</remarks>
        Friend Sub New()
            _previousCursor = Cursor.Current
            Cursor.Current = Cursors.WaitCursor
        End Sub 'Ne

        ''' <summary> Disposes the object, and restores the previous cursor. </summary>
        ''' <remarks> May be called multiple times safely. </remarks>
        Friend Sub Dispose() Implements IDisposable.Dispose
            If Not (_previousCursor Is Nothing) Then
                Cursor.Current = _previousCursor
                _previousCursor = Nothing
            Else
                Cursor.Current = Cursors.Default
            End If
        End Sub 'IDisposable.Dispose
    End Class 'WaitCursor

End Namespace
