<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateReview.aspx.cs" Inherits="ReelflicsWebsite.Member.CreateReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <div class="form-group">
            <div class="col-xs-12">
                <asp:Literal ID="litHeading" runat="server" Text="<h4 style=&quot;text-decoration: underline&quot;>Your Review for '" Visible="false"></asp:Literal>
            </div>
        </div>
        <!-- Review panel -->
        <asp:Panel ID="pnlReview" runat="server" Visible="false">
            <div class="form-group">
                <asp:Label ID="lblReviewTitle" runat="server" Text="Title" CssClass="control-label col-xs-1" AssociatedControlID="txtReviewText"></asp:Label>
                <div class="col-xs-6">
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control input-sm" MaxLength="50" ToolTip="Review Title"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="A review title is required."
                        ControlToValidate="txtTitle" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="lblRating" runat="server" Text="Rating" CssClass="control-label col-xs-2"
                    AssociatedControlID="ddlRating" Font-Names="Arial"></asp:Label>
                <div class="col-xs-3" style="margin-top: 2px">
                    <asp:DropDownList ID="ddlRating" runat="server" CssClass="dropdown input-sm" AutoPostBack="False" Height="25px" ToolTip="Rating">
                        <asp:ListItem Value="none selected">-- Select --</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Select a rating."
                        ControlToValidate="ddlRating" CssClass="text-danger" Display="Dynamic" InitialValue="none selected"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblReviewText" runat="server" Text="Review" CssClass="control-label col-xs-1" AssociatedControlID="txtReviewText"></asp:Label>
                <div class="col-xs-11">
                    <asp:TextBox ID="txtReviewText" runat="server" CssClass="form-control input-sm" Height="150px"
                        MaxLength="500" TextMode="MultiLine" ToolTip="Review Text"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Review is required."
                        ControlToValidate="txtReviewText" CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-11">
                    <asp:Button ID="btnCreateReview" runat="server" Text="Submit" Visible="True" CssClass="btn btn-sm" OnClick="BtnCreateReview_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
