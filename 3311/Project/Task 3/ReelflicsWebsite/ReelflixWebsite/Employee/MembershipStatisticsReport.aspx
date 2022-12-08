<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MembershipStatisticsReport.aspx.cs" Inherits="ReelflicsWebsite.Employee.MembershipStatisticsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Membership Statistics</h4>
        <br />
        <div class="form-group">
            <!-- Membership report panel -->
            <asp:Panel ID="pnlMembershipReport" runat="server" Visible="False">
                <div class="col-xs-offset-1 col-xs-4">
                    <asp:GridView ID="gvMembershipReport" runat="server" CssClass="table-condensed" CellPadding="5" CellSpacing="3"
                        BorderColor="Transparent" BackColor="Transparent" OnRowDataBound="GvMembershipReport_RowDataBound">
                    </asp:GridView>
                </div>
            </asp:Panel>
            <!-- Education level report panel -->
            <asp:Panel ID="pnlEducationLevelReport" runat="server" Visible="false">
                <div class="col-xs-6">
                    <asp:GridView ID="gvEducationLevelReport" runat="server" CssClass="table-condensed" CellPadding="5" CellSpacing="3"
                        BorderColor="Transparent" BackColor="Transparent" OnRowDataBound="GvEducationLevelReport_RowDataBound">
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
