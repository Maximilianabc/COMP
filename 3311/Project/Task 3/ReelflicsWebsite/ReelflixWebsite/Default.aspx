<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ReelflicsWebsite.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <!-- Top 10 Reelflics movies -->
        <asp:Panel ID="pnlMostWatchedMovies" runat="server" Visible="false">
            <h4 style="text-decoration: underline; margin-top: 30px; margin-bottom: 30px">Most Watched Movies on Reelflics</h4>
            <asp:PlaceHolder ID="phMostWatchedMovies" runat="server"></asp:PlaceHolder>
        </asp:Panel>
        <!-- Display splashscreen when no movies are retrieved or logged in user is employee -->
        <asp:Panel ID="pnlSplashscreen" runat="server" Visible="false">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <div style="background: transparent !important" class="jumbotron">
                <asp:Image runat="server" CssClass="img-responsive center-block" ImageUrl="~/images/splashscreen.jpg" Width="50%" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
