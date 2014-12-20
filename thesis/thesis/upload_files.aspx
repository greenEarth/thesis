<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload_files.aspx.cs" Inherits="upload_files" MasterPageFile="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

<table style="width: 606px">
    
    <tr>
    <td class="style1">Upload Your Picture</td>
    <td class="style2">
        <asp:FileUpload ID="FileUpload1" runat="server" style="margin-left: 0px" 
            Width="266px" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
    <td>Upload Your File that you want to save</td>
    <td>
        <asp:FileUpload ID="FileUpload2" runat="server" style="margin-left: 0px" 
            Width="266px" />
        </td></tr>
    
    <tr>
    <td class="style1">
        &nbsp;</td>
    <td class="style2">
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit" />
        </td>
    </tr>
    </table>



</asp:Content>