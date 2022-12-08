<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberInformationReport.aspx.cs" Inherits="ReelflicsWebsite.Employee.MemberInformationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <div style="margin-top: 20px">
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="label-info" Visible="False" Height="40px" BackColor="Transparent"></asp:Label>
        </div>
        <h4 style="text-decoration: underline">Member Information</h4>
        <br />
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
        <asp:Panel ID="pnlMemberAccountInformation" runat="server" Visible="false">
            <hr />
            <div class="form-group">
                <asp:Label runat="server" Text="Username" AssociatedControlID="litUsername" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litUsername" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Pseudonym" AssociatedControlID="litPseudonym" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litPseudonym" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="litFirstName" CssClass="col-xs-2 control-label">First name</asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litFirstName" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" AssociatedControlID="litLastName" CssClass="col-xs-2 control-label">Last name</asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litLastName" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Email" AssociatedControlID="litEmail" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Occupation" AssociatedControlID="litOccupation" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litOccupation" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="litGender" CssClass="col-xs-2 control-label">Gender</asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litGender" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Birthdate" AssociatedControlID="litBirthdate" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litBirthdate" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Phone number" AssociatedControlID="litPhoneNumber" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litPhoneNumber" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Education level" AssociatedControlID="litEducationLevel" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 8px">
                    <asp:Literal ID="litEducationLevel" runat="server"></asp:Literal>
                </div>
            </div>
            <hr />
            <!-- Credit card -->
            <h6>Credit Card Information</h6>
            <div class="form-group">
                <asp:Label runat="server" Text="Cardholder name" AssociatedControlID="litCardHolderName" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litCardHolderName" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Card number" AssociatedControlID="litCardNumber" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-3" style="margin-top: 10px">
                    <asp:Literal ID="litCardNumber" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Card type" AssociatedControlID="litCardType" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-2" style="margin-top: 10px">
                    <asp:Literal ID="litCardType" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Security code/CVV" AssociatedControlID="litSecurityCode" CssClass="col-xs-2 control-label"></asp:Label>
                <div class="col-xs-1" style="margin-top: 10px">
                    <asp:Literal ID="litSecurityCode" runat="server"></asp:Literal>
                </div>
                <asp:Label runat="server" Text="Expires" AssociatedControlID="litExpiryMonth" CssClass="col-xs-1 control-label"></asp:Label>
                <div class="col-xs-4" style="margin-top: 10px">
                    <asp:Literal ID="litExpiryMonth" runat="server"></asp:Literal> /
                    <asp:Literal ID="litExpiryYear" runat="server"></asp:Literal>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
