<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WatchNow.aspx.cs" Inherits="ReelflicsWebsite.Member.WatchNow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Error message display -->
    <div style="margin-top: 20px">
        <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
    </div>
    <!-- Enjoy the movie image -->
    <asp:Panel ID="pnlEnjoyMovie" runat="server" Visible="false">
        <div style="background: transparent !important" class="jumbotron">
            <div>
                <asp:Image runat="server" CssClass="img-responsive center-block" ImageUrl="~/images/enjoy.jpg" Width="80%" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
