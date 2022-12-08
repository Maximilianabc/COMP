<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Watchlist.aspx.cs" Inherits="ReelflicsWebsite.Member.Watchlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Your Watchlist</h4>
        <!-- Watchlist display -->
        <asp:Panel ID="pnlWatchlist" runat="server" Visible="false">
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvWatchlist" runat="server" CssClass="table-condensed" BorderColor="Transparent"
                        BackColor="Transparent" RowStyle-Wrap="False" OnRowDataBound="GvWatchlist_RowDataBound">
                    </asp:GridView>
                </div>
                <div class="col-xs-12">
                    <asp:Label ID="lblNoWatchlist" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
