<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberSearchResult.aspx.cs" Inherits="ReelflicsWebsite.MemberSearchResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h5>Titles</h5>
        <div class="form-group">
            <!-- Title search result -->
            <div class="col-xs-12">
                <asp:GridView ID="gvTitleSearchResult" runat="server" CssClass="table-condensed" Visible="False" CellPadding="5" CellSpacing="3" RowStyle-VerticalAlign="Top"
                    ShowHeader="False" BorderColor="Transparent" BackColor="Transparent" OnRowDataBound="GvTitleSearchResult_RowDataBound">
                </asp:GridView>
            </div>
            <div class="col-xs-12">
                <asp:Label ID="lblTitleSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
            </div>
        </div>
        <h5>Names</h5>
        <div class="form-group">
            <!-- Name search result -->
            <div class="col-xs-12">
                <asp:GridView ID="gvNameSearchResult" runat="server" CssClass="table-condensed" Visible="False" CellPadding="5" CellSpacing="3" RowStyle-VerticalAlign="Top"
                    ShowHeader="False" BackColor="Transparent" BorderColor="Transparent" OnRowDataBound="GvNameSearchResult_RowDataBound">
                </asp:GridView>
            </div>
            <div class="col-xs-12">
                <asp:Label ID="lblNameSearchResultMessage" runat="server" CssClass="label-info" Visible="False" BackColor="Transparent"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
