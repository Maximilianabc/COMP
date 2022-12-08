<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecommendedMovies.aspx.cs" Inherits="ReelflicsWebsite.Member.RecommendedMovies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div calss="col-xs-12" style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <!-- Recommended movies -->
        <asp:Panel ID="pnlRecommendedMovies" runat="server" Visible="false">
            <h4 style="text-decoration: underline; margin-top: 30px; margin-bottom: 30px">Recommended for You</h4>
            <div class="form-group" style="margin-top: 25px">
                <asp:PlaceHolder ID="phRecommendedMovies" runat="server"></asp:PlaceHolder>
            </div>
        </asp:Panel>
        <!-- Display splashscreen when there are no movies to recommend -->
        <asp:Panel ID="pnlSplashscreen" runat="server" Visible="false">
            <div style="background: transparent !important" class="jumbotron">
                <asp:Image runat="server" CssClass="img-responsive center-block" ImageUrl="~/images/splashscreen.jpg" Width="50%" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
