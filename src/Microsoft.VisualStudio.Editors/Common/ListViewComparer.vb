' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Option Strict On
Option Explicit On
Imports System.Windows.Forms

Namespace Microsoft.VisualStudio.Editors.Common

    ''' <summary>
    ''' IComparer for ListView. 
    ''' - Sort the ListView based on the current column or the first column if current column values are equal.
    ''' - Shared method to handle a column click event and sort the list view.
    ''' </summary>
    Friend Class ListViewComparer
        Implements IComparer

        '<Summary>Which column should be used to sort the list? Start from 0.</Summary>
        Public Property SortColumn() As Integer

        '<Summary>Which order, Ascending or Descending.</Summary>
        Public Property Sorting() As SortOrder = SortOrder.Ascending

        '<Summary>Compare two list items.</Summary>
        Public Function Compare(
                                 x As Object,
                                 y As Object
                               ) As Integer Implements System.Collections.IComparer.Compare
            Dim ret As Integer = String.Compare(GetColumnValue(x, _SortColumn), GetColumnValue(y, _SortColumn), StringComparison.OrdinalIgnoreCase)
            If ret = 0 AndAlso _SortColumn <> 0 Then ret = String.Compare(GetColumnValue(x, 0), GetColumnValue(y, 0), StringComparison.OrdinalIgnoreCase)
            If Sorting = SortOrder.Descending Then ret = -ret
            Return ret
        End Function

        '<Summary>Get String Value of one column.</Summary>
        Private Function GetColumnValue(
                                         obj As Object,
                                         column As Integer
                                        ) As String

            If TypeOf obj Is ListViewItem Then
                Dim listItem As ListViewItem = CType(obj, ListViewItem)
                Return listItem.SubItems.Item(column).Text
            End If

            Debug.Fail("RefComparer: obj was not an ListViewItem")
            Return String.Empty
        End Function

        Public Shared Sub HandleColumnClick(
                                             listView As ListView,
                                             comparer As ListViewComparer,
                                             e As ColumnClickEventArgs
                                           )

            Dim focusedItem As ListViewItem = listView.FocusedItem

            If e.Column <> comparer.SortColumn Then
                comparer.SortColumn = e.Column
                listView.Sorting = SortOrder.Ascending
            Else
                If listView.Sorting = SortOrder.Ascending Then
                    listView.Sorting = SortOrder.Descending
                Else
                    listView.Sorting = SortOrder.Ascending
                End If
            End If
            comparer.Sorting = listView.Sorting
            listView.Sort()

            If focusedItem IsNot Nothing Then
                listView.FocusedItem = focusedItem
            ElseIf listView.SelectedItems.Count > 0 Then
                listView.FocusedItem = listView.SelectedItems(0)
            End If

            If listView.FocusedItem IsNot Nothing Then listView.EnsureVisible(listView.FocusedItem.Index)

        End Sub
    End Class
End Namespace
