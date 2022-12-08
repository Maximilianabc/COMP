<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MovieInformation.aspx.cs" Inherits="ReelflicsWebsite.MovieInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <!-- Movie information diaplsy -->
        <asp:Panel ID="pnlMovieInformation" runat="server" Visible="false">
            <div class="form-group">
                <div class="col-xs-3" style="margin-top: 25px">
                    <asp:Image runat="server" ID="imgPoster" ImageUrl="~/images/posters/" CssClass="img-responsive center-block" Width="100%" BorderColor="White" BorderWidth="2px"></asp:Image>
                </div>
                <div class="col-xs-9">
                    <!-- Movie title and best picture indicator -->
                    <div class="row" style="color: white">
                        <div class="col-xs-12">
                            <h3>
                                <asp:Literal ID="litTitle" runat="server"></asp:Literal>
                                <asp:Image ID="imgBestPicture" runat="server" ImageUrl="~/images/oscar.png" Visible="false" BackColor="Transparent" BorderColor="Transparent" CssClass="img-thumbnail" Height="30px" />
                                <asp:Literal ID="litBestPicture" runat="server" Text="<span style=&quot;font-size: 16pt; color: goldenrod; margin-top: 0px&quot; > Best Picture</span>" Visible="false"></asp:Literal>
                            </h3>
                        </div>
                    </div>
                    <!-- Release year, running time, MPAA rating, genres -->
                    <div class="row" style="color: white">
                        <div class="col-xs-12">
                            <asp:Literal ID="litReleaseYear" runat="server"></asp:Literal>
                            <asp:Literal ID="litRunningTime" runat="server"></asp:Literal>
                            <asp:Literal ID="litMPAARating" runat="server"></asp:Literal>
                            <asp:Literal ID="litGenre" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <br />
                    <!-- IMDB rating, Reelflics rating, create/edit review link -->
                    <div class="row" style="color: white; margin-top: 0px">
                        <div class="col-xs-12">
                            <asp:Literal ID="litIMDBRating" runat="server" Text="IMDB Rating"></asp:Literal>
                            <asp:Literal ID="litReelflicsRating" runat="server" Visible="false"></asp:Literal>
                            <asp:HyperLink ID="hlCreateEditReview" runat="server" Visible="false"></asp:HyperLink>
                        </div>
                    </div>
                    <br />
                    <!-- Synopisis -->
                    <div class="row" style="margin-top: 0px">
                        <div class="col-xs-12">
                            <asp:Literal ID="litSynopsis" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <!-- Directors -->
                    <asp:Panel ID="pnlDirectors" runat="server" Visible="false">
                        <br />
                        <div class="row" style="color: white; margin-top: 0px;">
                            <div class="text-start col-xs-1">
                                Director
                            </div>
                            <div class="col-xs-11">
                                <asp:PlaceHolder ID="phDirectors" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Cast members -->
                    <asp:Panel ID="pnlCast" runat="server" Visible="false">
                        <br />
                        <div class="row" style="color: white; margin-top: 0px">
                            <div class="text-start col-xs-1">
                                Cast
                            </div>
                            <div class="col-xs-11">
                                <asp:PlaceHolder ID="phCast" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </asp:Panel>
                    <!-- Watchlist indicator -->
                    <asp:Panel ID="pnlWatchlist" runat="server" Visible="false">
                        <br />
                        <div class="row" style="margin-top: 0px">
                            <div class="col-xs-12">
                                <span style="color: white">On Your Watchlist</span>
                                <asp:Button ID="btnWatchlist" runat="server" Style="border: none !important; outline: none !important" BackColor="Transparent" CssClass="btn btn-sm"
                                    OnClick="BtnWatchlist_Click" Font-Bold="True" Font-Size="Large" CausesValidation="False" />
                            </div>
                        </div>
                        <br />
                        <div class="row" style="margin-top: 0px">
                            <div class="col-xs-12">
                                <asp:Literal ID="litLastWatched" runat="server" Text="<span style=&quot;color: white&quot;>Last watched</span>&nbsp;&nbsp;" Visible="false"></asp:Literal>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <!-- Watch now button -->
            <asp:Panel ID="pnlWatchNow" runat="server" Visible="false">
                <div class="form-group">
                    <div class="col-xs-3">
                        <asp:Button ID="btnWatchNow" runat="server" BackColor="Transparent" CssClass="btn btn-sm center-block" Font-Size="Medium" BorderColor="White"
                            BorderWidth="2px" CausesValidation="False" OnClick="BtnWatchNow_Click" />
                    </div>
                </div>
            </asp:Panel>
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:Button ID="btnReviews" runat="server" CssClass="btn-link" Visible="false" OnClick="BtnReviews_Click" CausesValidation="False" />
                </div>
            </div>
            <!-- Reviews panel -->
            <asp:Panel ID="pnlReviews" runat="server" Visible="false">
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:PlaceHolder ID="phReviews" runat="server"></asp:PlaceHolder>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
