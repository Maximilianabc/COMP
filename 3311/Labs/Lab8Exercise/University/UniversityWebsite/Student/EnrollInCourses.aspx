<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnrollInCourses.aspx.cs" Inherits="UniversityWebsite.Enrollment.EnrollInCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblQueryResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False" Height="40px"></asp:Label>
        </div>
        <h4><span style="text-decoration: underline; color: #990000">Courses You Can Enroll In</span></h4>
        <br />
        <asp:Panel ID="pnlAvailableCourses" runat="server" Visible="False">
            <br />
            <div class="form-group">
                <div class="col-xs-12">
                    <asp:GridView ID="gvAvailableCourses" runat="server" BorderWidth="2px" CssClass="table-condensed" BorderStyle="Solid"
                        CellPadding="3" HorizontalAlign="Justify" Visible="false" OnRowDataBound="GvAvailableCourses_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SELECT">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblNoAvailableCoursesMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div class="col-xs-1">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="Submit" CssClass="btn-sm" />
                </div>
                <div class="col-xs-11">
                    <asp:Label ID="lblSubmitMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
