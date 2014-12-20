<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <%--<p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %>characters in length.
                    </p>--%>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                             <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="Email" ErrorMessage="Not an E-mail" 
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                            <p>
                            <asp:Label ID="Label1" runat="server" >Contact Number</asp:Label>
                                 <asp:TextBox ID="contact_text" runat="server" CssClass="textEntry"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="contactRequird" runat="server" ControlToValidate="contact_text" 
                                     CssClass="failureNotification" ErrorMessage="contact number" ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                
                            </p>
                            
                            <p>
                            <cc1:ToolkitScriptManager ID="p" runat="server"></cc1:ToolkitScriptManager>
                            <asp:Label ID="Label2" runat="server" >Date OF Birth</asp:Label>
                                 <asp:TextBox ID="dob_text" runat="server" CssClass="textEntry" 
                                    TextMode="SingleLine"></asp:TextBox>
                                    <cc1:CalendarExtender ID="date1" runat="server" TargetControlID="dob_text" PopupButtonID="dob_text"></cc1:CalendarExtender>
                                
                                <asp:RequiredFieldValidator ID="DOBRequired1" runat="server" ControlToValidate="dob_text" 
                                     CssClass="failureNotification" ErrorMessage="is Required" ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                
                            </p>
                          
                            <p>
                            <asp:Label ID="Label3" runat="server" >Address</asp:Label>
                                 <asp:TextBox ID="address_text" runat="server" CssClass="textEntry"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="addressRequired" runat="server" ControlToValidate="address_text" 
                                     CssClass="failureNotification" ErrorMessage="Address is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                
                            </p>
                           
                            <p>
                                <%-- <asp:Label ID="Label4" runat="server" >Upload Your Secret Picture</asp:Label>
                                 <asp:FileUpload ID="FileUpload1" runat="server" Width="319px" />
                                
                                <asp:RequiredFieldValidator ID="FileRequired" runat="server" ControlToValidate="FileUpload1" 
                                     CssClass="failureNotification" ErrorMessage="Upload A Picture" ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>--%>
                                
                            <asp:Label ID="Label5" runat="server" >Type The Code</asp:Label>
                                 
                                 
                                 <asp:Label ID="Image1" runat="server" BorderColor="#009900" 
                                    BorderStyle="Groove" BackColor="#FF0066" ForeColor="#CCFFCC" 
                                    Font-Size="XX-Large" Font-Bold="True" Font-Strikeout="True" 
                                    
                                    style="font-family: 'Times New Roman'; vertical-align:middle; text-align:center" 
                                    Height="20px" Width="100px"/>
                                <asp:Button ID="Button1" runat="server" Text="Reload Captcha" 
                                    onclick="Button1_Click" />
                                 <asp:TextBox ID="captcha_code_text" runat="server" CssClass="textEntry"></asp:TextBox>
                                
                                <asp:RequiredFieldValidator ID="CaptchaRequired" runat="server" ControlToValidate="captcha_code_text" 
                                     CssClass="failureNotification" ErrorMessage="Enter Code" ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                
                            </p>
                            
                            
                           
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup" 
                                onclick="CreateUserButton_Click"/>
                        </p>
                        <p class="submitButton">
                            &nbsp;</p>
                    </div>
                
                
</asp:Content>