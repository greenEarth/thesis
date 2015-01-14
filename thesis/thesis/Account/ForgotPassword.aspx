<%@ Page Title="ForgotPassword" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="True" Inherits="Account_ForgotPassword" Codebehind="ForgotPassword.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
                    
                    <p>
                        Please provide email address assosiated with the account.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="ForgotPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="ForgotPasswordValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="emailPanel">
                            <p>
                                <asp:Label ID="emailLabel" runat="server" AssociatedControlID="email" >Email Address:</asp:Label>
                                <asp:TextBox ID="email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="emailRequired" runat="server" ControlToValidate="email" 
                                     CssClass="failureNotification" ErrorMessage="Email address is required." ToolTip="Email address is required." 
                                     ValidationGroup="ForgotPasswordValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                    ControlToValidate="email" ErrorMessage="Not an E-mail" ValidationGroup="ForgotPasswordValidationGroup"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="emailNotExists" runat="server" ValidateEmptyText="true" ControlToValidate="email" OnServerValidate="emailExistsOrNot"
                                    CssClass="failureNotification" ErrorMessage="Email not exists in the system." ToolTip="Email not exists in the system."
                                    ValidationGroup="ForgotPasswordValidationGroup">*</asp:CustomValidator>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="SubmitButton" runat="server" CommandName="MoveNext" Text="Retrieve Password" 
                                 ValidationGroup="ForgotPasswordValidationGroup" 
                                onclick="forgotPassButton_Click"/>
                        </p>
                        <p class="submitButton">
                            &nbsp;</p>
                    </div>
                
                
</asp:Content>