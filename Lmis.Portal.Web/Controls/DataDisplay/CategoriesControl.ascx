<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesControl.ascx.cs" Inherits="Controls_DataDisplay_CategoriesControl" %>
<ce:TreeView runat="server" KeyFieldName="ID" ParentFieldName="ParentID" TextFieldName="Name" ID="tvData" OnSelectedNodeChanged="tvData_OnSelectedNodeChanged"></ce:TreeView>
