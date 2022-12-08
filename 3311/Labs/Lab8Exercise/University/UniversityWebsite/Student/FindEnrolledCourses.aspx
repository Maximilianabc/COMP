<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FindEnrolledCourses.aspx.cs" Inherits="UniversityWebsite.Enrollment.FindEnrolledCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblQueryResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False" Height="40px"></asp:Label>
        </div>
        <h4><span style="text-decoration: underline; color: #990000">Courses You Are Enrolled In</span></h4>
        <br />
        <asp:Panel ID="pnlEnrolledCourses" runat="server" Visible="False">
            <br />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvEnrolledCourses" runat="server" BorderWidth="2px" CssClass="table-condensed" BorderStyle="Solid"
                        CellPadding="3" OnRowDataBound="GvEnrolledCourses_RowDataBound">
                    </asp:GridView>
                    <asp:Label ID="lblNoEnrolledCoursesMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
