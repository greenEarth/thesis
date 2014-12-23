<%@ Page Title="Your Drive" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" Inherits="About" Codebehind="About.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="p" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="files_details" runat="server">
      
        <ContentTemplate>
    <div style="height:65px; background-color:  #4b6c9e;" >
      <table border="0px" cellpadding="0px"> 
      <tr>
       <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="Download_files_btn" runat="server" Height="31px" 
            ImageUrl="../Images/Download.png" Width="36px" ToolTip="Download Files" 
            onclick="Download_files_btn_Click" /></td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton 
            ID="upload_files_btn" runat="server" Height="38px" 
            ImageUrl="Images/Upload-icon.png" Width="41px" ToolTip="Upload Files" 
            onclick="upload_files_btn_Click" /></td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="recent_files_btn" runat="server" Height="31px" 
            ImageUrl="Images/Folder My Recent Documents.png" Width="41px" 
            ToolTip="View Recent Files" onclick="recent_files_btn_Click" /></td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="sort_name_btn" runat="server" Height="31px" 
            ImageUrl="Images/sort_neutral_green.png" Width="35px" 
            ToolTip="Sort By Name" onclick="sort_name_btn_Click" /></td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="sort_size_btn" runat="server"  Height="31px" 
            ImageUrl="Images/sort_neutral_green.png" Width="35px"
            ToolTip="Sort By Size" onclick="sort_size_btn_Click"/></td>
        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="sort_type_btn" runat="server"  Height="31px" 
            ImageUrl="Images/sort_neutral_green.png" Width="35px"
            ToolTip="Sort By Type" onclick="sort_type_btn_Click" /></td>
        </tr>
       <tr > 
           <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="Delete Files" 
                   ForeColor="WhiteSmoke"></asp:Label></td>
           <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
                   ID="Label2" runat="server" Text="Upload Files" ForeColor="WhiteSmoke"></asp:Label></td>
           <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="Recent Files" 
                   ForeColor="WhiteSmoke"></asp:Label></td>
           <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label4" runat="server" Text="Sort By Name" 
                   ForeColor="WhiteSmoke"></asp:Label></td>
           <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Sort By Size" 
                   ForeColor="WhiteSmoke"></asp:Label></td>
           <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="Sort By Type" 
                   ForeColor="WhiteSmoke"></asp:Label></td>
        </tr>
        </table>
        <br />
    
    </div>
    <h2>
        Your Files...
    </h2>
    
   <div align="center">
   <asp:GridView ID="GridView1" runat="server" CellPadding="0"
            EnableModelValidation="True" Font-Size="Small" GridLines="None">
            <EditRowStyle HorizontalAlign="Center" Width="200px" Wrap="True" />
            <HeaderStyle BorderStyle="None" Font-Size="0pt" Width="0px" Wrap="True" />
            <PagerStyle HorizontalAlign="Center" Wrap="True" />
            <RowStyle HorizontalAlign="Center" Width="200px" Wrap="True" />
        </asp:GridView>
   <br />
       <asp:Panel ID="Panel1" runat="server" Height="142px" Visible="False">
          <asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="350"  />
         <asp:BoundField DataField="Extension" HeaderText="Extension" ItemStyle-Width="150"  />
        <asp:TemplateField HeaderText="id" ItemStyle-Width="150" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_id" runat="server" Text='<%# Eval("id") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
           <br />
           <asp:Label ID="messagse_label" text="" runat="server"></asp:Label>
           <asp:Button ID="DeleteSelected" runat="server" Text="Delete Selected Files" OnClick="DeleteSelected_Click" />
       </asp:Panel>
       <br />
        <br />
       <asp:Panel ID="upload_panel" runat="server" Visible="False">

           <table style="width: 606px">
    <tr><td colspan="2"><asp:Label ID="Label7" runat="server"></asp:Label></td><td></td></tr>
    <tr>
    <td class="style1">Upload Your Picture</td>
    <td class="style2">
        <asp:FileUpload ID="FileUpload1" onchange="Button1_Click();" runat="server" style="margin-left: 0px" 
            Width="266px" />
        
        </td>
    </tr>
    <tr>
    <td>Upload Your File that you want to save</td>
    <td>
        <asp:FileUpload ID="FileUpload2" onchange="Button1_Click();" runat="server" style="margin-left: 0px" 
            Width="266px" />
        </td></tr>
    
    <tr>
    <td class="style1">
        &nbsp;</td>
    <td class="style2">
        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
        </td>
    </tr>
    </table>
       </asp:Panel>
   </div> 
      </ContentTemplate>
          <Triggers>
               <asp:PostBackTrigger ControlID="Button1" />
           </Triggers>
    </asp:UpdatePanel>
   </asp:Content>

   <%-- backup of grid 
       <asp:GridView ID="GridView3" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
    AutoGenerateColumns="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="150"  />
        <asp:TemplateField HeaderText="Extension" ItemStyle-Width="150">
            <ItemTemplate>
                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Extension") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>--%>
