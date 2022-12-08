<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FindDepartmentStudentRecords.aspx.cs" Inherits="UniversityWebsite.Student.FindDepartmentStudentRecords" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblQueryResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False" Height="40px"></asp:Label>
        </div>
        <h4><span style="text-decoration: underline; color: #990000">Find Student Records In A Department</span></h4>
        <br />
        <asp:Panel ID="pnlSearchCriteria" runat="server" Visible="false">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlDepartments" CssClass="col-xs-2 control-label" Font-Names="Arial">Department:</asp:Label>
                <div class="col-xs-10">
                    <asp:DropDownList ID="ddlDepartments" runat="server" CssClass="dropdown"
                        OnSelectedIndexChanged="DdlDepartments_SelectedIndexChanged" AutoPostBack="True" CausesValidation="True" Font-Names="Arial" Font-Size="Small">
                    </asp:DropDownList>
                    <asp:Label ID="lblNoStudentsEnrolledMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
                </div>
                <div class="col-xs-offset-2 col-xs-10">
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Please select a department."
                        ControlToValidate="ddlDepartments" CssClass="text-danger" Display="Dynamic" InitialValue="none selected" ToolTip="Department"
                        EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSearchResult" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-11">
                    <asp:GridView ID="gvFindStudentRecordsResult" runat="server" BorderWidth="2px" CssClass="table-condensed"
                        BorderStyle="Solid" OnRowDataBound="GvFindStudentRecordsResult_RowDataBound" Font-Names="Arial" Font-Size="Small">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
