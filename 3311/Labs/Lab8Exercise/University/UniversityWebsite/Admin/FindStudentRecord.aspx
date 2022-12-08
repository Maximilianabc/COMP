<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FindStudentRecord.aspx.cs" Inherits="UniversityWebsite.Student.FindStudentRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <!-- Error message display -->
        <div style="margin-top: 20px">
            <asp:Label ID="lblQueryResultMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False" Height="40px"></asp:Label>
        </div>
        <h4><span style="text-decoration: underline; color: #990000">Find Student Record</span></h4>
        <br />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtStudentId" CssClass="col-xs-2 control-label" Font-Names="Arial">Student Id:</asp:Label>
            <div class="col-xs-2">
                <asp:TextBox ID="txtStudentId" runat="server" CssClass="form-control input-sm" MaxLength="8" ToolTip="Student Id" Font-Names="Arial"></asp:TextBox>
            </div>
            <div class="col-xs-1">
                <asp:Button ID="btnFind" runat="server" OnClick="BtnFindStudentRecord_Click" Text="Find" CssClass="btn-sm" Font-Names="Arial" />
            </div>
            <asp:Label ID="lblNoStudentRecordMessage" runat="server" Font-Bold="True" CssClass="label" Font-Names="Arial Narrow" Font-Size="Small" Visible="False"></asp:Label>
        </div>
        <div class="form-group">
            <div class="col-xs-offset-2 col-xs-10">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Please enter a valid student id." ControlToValidate="txtStudentId"
                    CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ErrorMessage="Please enter exactly 8 digits."
                    ControlToValidate="txtStudentId" CssClass="text-danger" Display="Dynamic" EnableClientScript="False" Font-Names="Arial" Font-Size="Small"
                    ValidationExpression="^\d{8}$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <asp:Panel ID="pnlStudentRecord" runat="server" Visible="False">
            <hr />
            <div class="form-group">
                <div class="col-xs-offset-1 col-xs-11">
                    <asp:GridView ID="gvStudentRecord" runat="server" BorderWidth="2px" CssClass="table-condensed" BorderStyle="Solid"
                        CellPadding="3" Font-Names="Arial" Font-Size="Small" OnRowDataBound="gvStudentRecord_RowDataBound">
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
