<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberActivityReport.aspx.cs" Inherits="ReelflicsWebsite.Employee.MemberActivityReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Member Activity</h4>
        <br />
        <!-- Member name dropdown -->
        <asp:Panel ID="pnlMemberName" runat="server" Visible="false">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlMemberName" CssClass="col-xs-2 control-label">Member name</asp:Label>
                <div class="col-xs-3" style="margin-top: 6px">
                    <asp:DropDownList ID="ddlMemberName" runat="server" CssClass="dropdown input-sm" Height="25px" AutoPostBack="True"
                        CausesValidation="True" OnSelectedIndexChanged="DdlMemberName_SelectedIndexChanged" ToolTip="Member name">
                    </asp:DropDownList>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMemberName" ErrorMessage="Select a name."
                            CssClass="text-danger" Display="Dynamic" EnableClientScript="False" InitialValue="none selected"
                            BackColor="Transparent"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <!-- Activity report -->
        <asp:Panel ID="pnlActivityReport" runat="server" Visible="false">
            <!-- Watch history report -->
            <hr />
            <h5><span style="text-decoration: underline">Watch History</span>
                <asp:Button ID="btnWatchHistoryReport" runat="server" CssClass="btn-link" OnClick="BtnWatchHistoryReport_Click"
                    CausesValidation="False" Font-Size="Small" /></h5>
            <asp:Panel ID="pnlWatchHistory" runat="server" Visible="false">
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvWatchHistory" runat="server" CssClass="table-condensed" Visible="false" BorderColor="Transparent"
                            BackColor="Transparent" RowStyle-Wrap="False" OnRowDataBound="GvWatchHistory_RowDataBound" AllowPaging="True"
                            AllowSorting="True" OnSorting="GvWatchHistory_Sorting" OnPageIndexChanging="GvWatchHistory_PageIndexChanging" PageSize="5">
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12">
                        <asp:Label ID="lblNoWatchHistory" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <!-- Genre viewing report -->
            <hr />
            <h5><span style="text-decoration: underline">Genre Watch Statistics</span>
                <asp:Button ID="btnGenreViewingStatisticsReport" runat="server" CssClass="btn-link" OnClick="BtnGenreViewingStatisticsReport_Click"
                    CausesValidation="False" Font-Size="Small" /></h5>
            <asp:Panel ID="pnlGenreViewingStatisticsReport" runat="server" Visible="false">
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvGenreViewingStatisticsReport" runat="server" CssClass="table-condensed" BorderColor="Transparent"
                            BackColor="Transparent" RowStyle-Wrap="False" ShowHeader="False" OnRowDataBound="GvGenreViewingStatisticsReport_RowDataBound">
                            <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                            <RowStyle HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
            <!-- Watchlist report -->
            <hr />
            <h5><span style="text-decoration: underline">Watchlist</span>
                <asp:Button ID="btnWatchlistReport" runat="server" CssClass="btn-link" OnClick="BtnWatchlistReport_Click"
                    CausesValidation="False" Font-Size="Small" /></h5>
            <asp:Panel ID="pnlWatchlist" runat="server" Visible="false">
                <div class="form-group">
                    <div class="col-xs-12">
                        <asp:GridView ID="gvWatchlist" runat="server" CssClass="table-condensed" Visible="false" BorderColor="Transparent"
                            BackColor="Transparent" RowStyle-Wrap="False" OnRowDataBound="GvWatchlist_RowDataBound" AllowPaging="True" 
                            OnPageIndexChanging="GvWatchlist_PageIndexChanging" PageSize="5">
                        </asp:GridView>
                    </div>
                    <div class="col-xs-12">
                        <asp:Label ID="lblNoWatchlist" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
