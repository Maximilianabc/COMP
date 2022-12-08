<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoviePersonInformation.aspx.cs" Inherits="ReelflicsWebsite.MoviePersonInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <!-- Movie Person information -->
        <asp:Panel ID="pnlMoviePersonInformation" runat="server" Visible="false">
            <div class="form-group">
                <div class="col-xs-2" style="margin-top: 25px">
                    <asp:Image runat="server" ID="imgPhoto" ImageUrl="~/images/people/" CssClass="img-responsive center-block" Width="100%" BorderColor="White" BorderWidth="2px"></asp:Image>
                </div>
                <div class="col-xs-10">
                    <!-- Name -->
                    <div class="row col-xs-12">
                        <h3>
                            <asp:Literal ID="litName" runat="server"></asp:Literal>
                        </h3>
                    </div>
                    <!-- Biography -->
                    <div class="row col-xs-12">
                        <asp:Literal ID="litBiography" runat="server"></asp:Literal>
                    </div>
                    <!-- Gender -->
                    <div class="row col-xs-12" style="margin-top: 10px">
                        <asp:Literal ID="litGender" runat="server" Text="<span style=&quot;color: white&quot;>Gender:</span> "></asp:Literal>
                    </div>
                    <!-- Birthdate -->
                    <div class="row col-xs-12" style="margin-top: 10px">
                        <asp:Literal ID="litBirthdate" runat="server" Text="<span style=&quot;color: white&quot;>Born:</span> "></asp:Literal>
                    </div>
                    <!-- Deathdate -->
                    <div class="row col-xs-12" style="margin-top: 10px">
                        <asp:Literal ID="litDeathdate" runat="server" Text="<span style=&quot;color: white&quot;>Died:</span> " Visible="false"></asp:Literal>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Filmography -->
        <asp:Panel ID="pnlFilmography" runat="server" Visible="false">
            <h5 style="margin-top: 20px">Filmography</h5>
            <!-- Actor/actress filmography -->
            <asp:Panel ID="pnlActorActressFilmography" runat="server" Visible="false">
                <h6 style="color: navajowhite; margin-top: 12px">
                    <asp:Literal ID="litActorActressHeading" runat="server" Text="Actor"></asp:Literal></h6>
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvActorActressFilmography" runat="server" CssClass="table-condensed" Visible="False" RowStyle-VerticalAlign="Top"
                            ShowHeader="False" OnRowDataBound="GvActorActressFilmography_RowDataBound" BorderColor="Transparent" BackColor="Transparent">
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
            <!-- Director filmography -->
            <asp:Panel ID="pnlDirectorFilmography" runat="server" Visible="false">
                <h6 style="color: navajowhite; margin-top: 12px">Director</h6>
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvDirectorFilmography" runat="server" CssClass="table-condensed" Visible="False" RowStyle-VerticalAlign="Top"
                            ShowHeader="False" OnRowDataBound="GvDirectorFilmography_RowDataBound" BackColor="Transparent" BorderColor="Transparent">
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
