<%@ Page Title="Brand Management"
    Language="C#"
    MasterPageFile="~/Phonefit.Master"
    AutoEventWireup="true"
    CodeBehind="ManagerBrands.aspx.cs"
    Inherits="PhoneFit.ManagerBrands" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .brand-table-wrapper {
            overflow-x: auto;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .brand-table {
            width: 100%;
            border-collapse: collapse;
        }

        .brand-table th,
        .brand-table td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid var(--rule);
        }

        .brand-table th {
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--fg-mute);
            background: var(--bg);
        }

        .brand-table td {
            font-size: 14px;
        }

        .status-active,
        .status-inactive {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 999px;
            font-size: 12px;
            font-weight: 600;
        }

        .status-active {
            color: #087443;
            background: #dff7eb;
        }

        .status-inactive {
            color: #a11a1a;
            background: #fde5e5;
        }

                .form-section {
            max-width: 700px;
            padding: 24px;
            margin-bottom: 30px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .form-section h2 {
            margin-bottom: 20px;
        }

        .form-field {
            margin-bottom: 18px;
        }

        .form-field label {
            display: block;
            margin-bottom: 6px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .form-field input,
        .form-field textarea {
            width: 100%;
            padding: 11px 13px;
            border: 1px solid var(--rule-strong);
            border-radius: var(--r-sm);
        }
    </style>

</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">
    <main id="main">
        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="ManagerProducts.aspx">Product Management</a>
                    <span class="sep">›</span>
                    <span>Brand Management</span>
                </div>
                <h1>Brand Management</h1>
                <p>
                    View and manage the smartphone brands available
                    in the PhoneFit catalogue.
                </p>
            </div>
        </section>
        <section class="section">
            <div class="container">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red">
                </asp:Label>
                <asp:Panel ID="pnlNoBrands" runat="server" Visible="false">
                    <div style="
                        padding: 40px;
                        text-align: center;
                        background: var(--bg);
                        border-radius: var(--r);">
                        No brands were found.
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlBrands"
                    runat="server">
                    <div class="form-section">
                        <h2>Add Brand</h2>

                        <div class="form-field">
                            <label>Brand Name *</label>
                            <asp:TextBox
                                ID="txtBrandName"
                                runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvBrandName"
                                runat="server"
                                ControlToValidate="txtBrandName"
                                ErrorMessage="Brand name is required."
                                ForeColor="Red"
                                ValidationGroup="BrandGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-field">
                            <label>Brand Description</label>

                            <asp:TextBox
                                ID="txtBrandDescription"
                                runat="server"
                                TextMode="MultiLine"
                                Rows="3">
                            </asp:TextBox>
                        </div>

                        <asp:Button
                            ID="btnAddBrand"
                            runat="server"
                            Text="Add Brand"
                            CssClass="btn btn--indigo"
                            ValidationGroup="BrandGroup"
                            OnClick="btnAddBrand_Click" />
                    </div>
                    <div class="brand-table-wrapper">
                        <table class="brand-table">
                            <thead>
                                <tr>
                                    <th>Brand ID</th>
                                    <th>Brand Name</th>
                                    <th>Description</th>
                                    <th>Status</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptBrands" runat="server" OnItemCommand="rptBrands_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <%# Eval("BrandID") %>
                                            </td>
                                            <td>
                                                <%# Eval("BrandName") %>
                                            </td>
                                            <td>
                                                <%# Eval("BrandDescription") %>
                                            </td>
                                            <td>
                                                <asp:Label
                                                    ID="lblBrandStatus"
                                                    runat="server"
                                                    Text='<%# Convert.ToBoolean(Eval("BrandIsActive")) ? "Active" : "Inactive" %>'
                                                    CssClass='<%# Convert.ToBoolean(Eval("BrandIsActive")) ? "status-active" : "status-inactive" %>'>
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <a href='<%# "ManagerEditBrand.aspx?id=" + Eval("BrandID") %>'
                                                 class="btn btn--ghost btn--sm">Edit</a>

                                                <asp:LinkButton
                                                    ID="btnChangeBrandStatus"
                                                    runat="server"
                                                    CommandName="ChangeStatus"
                                                    CommandArgument='<%# Eval("BrandID") + "," + Eval("BrandIsActive") %>'
                                                    Text='<%# Convert.ToBoolean(Eval("BrandIsActive")) ? "Deactivate" : "Reactivate" %>'
                                                    CssClass="btn btn--ghost btn--sm"
                                                    CausesValidation="false">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </asp:Panel>
            </div>
        </section>
    </main>
</asp:Content>
