<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ReelflicsWebsite.Account.Login" Async="true" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="form-horizontal">
        <h4 style="text-decoration: underline">Log in</h4>
        <div class="form-group">
            <div class="row">
                <asp:Label runat="server" Text="Username" AssociatedControlID="UserName" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-2" style="margin-top: 4px">
                    <asp:TextBox runat="server" ID="UserName" CssClass="form-control input-sm" Height="30px" MaxLength="10" Width="120px" />
                </div>
                <div class="col-xs-1" style="margin-top: 4px">
                    <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-sm" Height="30px" />
                </div>
                <div class="col-xs-7" style="margin-top: 8px">
                    <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled" CssClass="btn-link">Create a Reelflics account</asp:HyperLink>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-offset-2 col-xs-10">
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" ErrorMessage="Username is required." CssClass="text-danger"
                        EnableClientScript="False" Display="Dynamic" />
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <div class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </div>
                    </asp:PlaceHolder>
                </div>
            </div>
            <div class="row">
            </div>
        </div>
    </div>
</asp:Content>
