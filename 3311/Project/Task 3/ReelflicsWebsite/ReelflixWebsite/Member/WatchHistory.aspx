<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WatchHistory.aspx.cs" Inherits="ReelflicsWebsite.Member.WatchHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Your Watch History</h4>
        <!-- Viewing history -->
        <asp:Panel ID="pnlWatchHistory" runat="server" Visible="false">
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvWatchHistory" runat="server" CssClass="table-condensed" Visible="False" BorderColor="Transparent" 
                        BackColor="Transparent" RowStyle-Wrap="False" OnRowDataBound="GvWatchHistory_RowDataBound" AllowPaging="True" AllowSorting="True"
                        OnSorting="GvWatchHistory_Sorting" OnPageIndexChanging="GvWatchHistory_PageIndexChanging">
                    </asp:GridView>
                </div>
                <div class="col-xs-12">
                    <asp:Label ID="lblNoWatchHistory" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
