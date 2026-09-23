<%@ Page Title="Variant Management" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerVariants.aspx.cs" Inherits="PhoneFit.ManagerVariants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .variant-table-wrapper {
            overflow-x: auto;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .variant-table {
            width: 100%;
            border-collapse: collapse;
        }

        .variant-table th,
        .variant-table td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid var(--rule);
        }

        .variant-table th {
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--fg-mute);
            background: var(--bg);
        }

        .variant-table td {
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

        .variant-form {
            max-width: 850px;
            padding: 24px;
            margin-bottom: 30px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .variant-form h2 {
            margin-bottom: 20px;
        }

        .form-row {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 18px;
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

        .form-field input {
            width: 100%;
            padding: 11px 13px;
            border: 1px solid var(--rule-strong);
            border-radius: var(--r-sm);
            background: var(--paper);
        }

        @media (max-width: 650px) {
            .form-row {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">
        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="ManagerProducts.aspx">Product Management</a>
                    <span class="sep">›</span>
                    <span>Variant Management</span>
                </div>
                <h1>
                    <asp:Label ID="lblPhoneName" runat="server"></asp:Label>
                </h1>
                <p>
                    View the variants currently linked to this smartphone.
                </p>
            </div>
        </section>
        <section class="section">
            <div class="container">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                <div class="variant-form">
                    <h2>Add New Variant</h2>
                    <div class="form-row">
                        <div class="form-field">
                            <label>RAM (GB) *</label>
                            <asp:TextBox
                                ID="txtRAM"
                                runat="server"
                                TextMode="Number">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvRAM"
                                runat="server"
                                ControlToValidate="txtRAM"
                                ErrorMessage="RAM is required."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator
                                ID="rvRAM"
                                runat="server"
                                ControlToValidate="txtRAM"
                                Type="Integer"
                                MinimumValue="1"
                                MaximumValue="128"
                                ErrorMessage="RAM must be greater than 0."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RangeValidator>
                        </div>
                        <div class="form-field">
                            <label>Storage (GB) *</label>
                            <asp:TextBox
                                ID="txtStorage"
                                runat="server"
                                TextMode="Number">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvStorage"
                                runat="server"
                                ControlToValidate="txtStorage"
                                ErrorMessage="Storage is required."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator
                                ID="rvStorage"
                                runat="server"
                                ControlToValidate="txtStorage"
                                Type="Integer"
                                MinimumValue="1"
                                MaximumValue="4096"
                                ErrorMessage="Storage must be greater than 0."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RangeValidator>
                        </div>
                    </div>
                    <div class="form-field">
                        <label>Colour *</label>
                        <asp:TextBox
                            ID="txtColour"
                            runat="server">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfvColour"
                            runat="server"
                            ControlToValidate="txtColour"
                            ErrorMessage="Colour is required."
                            ForeColor="Red"
                            ValidationGroup="VariantGroup">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="form-row">
                        <div class="form-field">
                            <label>Price (R) *</label>
                            <asp:TextBox
                                ID="txtPrice"
                                runat="server"
                                TextMode="Number">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvPrice"
                                runat="server"
                                ControlToValidate="txtPrice"
                                ErrorMessage="Price is required."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="form-field">
                            <label>Stock Quantity *</label>
                            <asp:TextBox
                                ID="txtStockQuantity"
                                runat="server"
                                TextMode="Number">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfvStockQuantity"
                                runat="server"
                                ControlToValidate="txtStockQuantity"
                                ErrorMessage="Stock quantity is required."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator
                                ID="rvStockQuantity"
                                runat="server"
                                ControlToValidate="txtStockQuantity"
                                Type="Integer"
                                MinimumValue="0"
                                MaximumValue="100000"
                                ErrorMessage="Stock quantity cannot be negative."
                                ForeColor="Red"
                                ValidationGroup="VariantGroup">
                            </asp:RangeValidator>
                        </div>
                    </div>
                    <div class="form-field">
                        <label>Low Stock Level *</label>
                        <asp:TextBox
                            ID="txtLowStockLevel"
                            runat="server"
                            TextMode="Number">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfvLowStockLevel"
                            runat="server"
                            ControlToValidate="txtLowStockLevel"
                            ErrorMessage="Low stock level is required."
                            ForeColor="Red"
                            ValidationGroup="VariantGroup">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator
                            ID="rvLowStockLevel"
                            runat="server"
                            ControlToValidate="txtLowStockLevel"
                            Type="Integer"
                            MinimumValue="0"
                            MaximumValue="100000"
                            ErrorMessage="Low stock level cannot be negative."
                            ForeColor="Red"
                            ValidationGroup="VariantGroup">
                        </asp:RangeValidator>
                    </div>
                    <asp:Button
                        ID="btnAddVariant"
                        runat="server"
                        Text="Add Variant"
                        CssClass="btn btn--indigo"
                        ValidationGroup="VariantGroup"
                        OnClick="btnAddVariant_Click" />
                </div>
                <asp:Panel ID="pnlNoVariants" runat="server" Visible="false">
                    <div style="padding:40px; text-align:center; background:var(--bg); border-radius:var(--r);">
                        No variants were found for this smartphone.
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlVariants" runat="server">
                    <div class="variant-table-wrapper">
                        <table class="variant-table">
                            <thead>
                                <tr>
                                    <th>Variant ID</th>
                                    <th>RAM</th>
                                    <th>Storage</th>
                                    <th>Colour</th>
                                    <th>Price</th>
                                    <th>Stock</th>
                                    <th>Low Stock Level</th>
                                    <th>Status</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptVariants" runat="server" OnItemCommand="rptVariants_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("VariantID") %></td>
                                            <td><%# Eval("RAMGB") %> GB</td>
                                            <td><%# Eval("StorageGB") %> GB</td>
                                            <td><%# Eval("Colour") %></td>
                                            <td>R<%# Eval("Price", "{0:N2}") %></td>
                                            <td><%# Eval("StockQuantity") %></td>
                                            <td><%# Eval("LowStockLevel") %></td>
                                            <td>
                                                <asp:Label
                                                    ID="lblVariantStatus"
                                                    runat="server"
                                                    Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>'
                                                    CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "status-active" : "status-inactive" %>'>
                                                </asp:Label>
                                            </td>
                                            <td><a href='<%# "ManagerEditVariant.aspx?id=" + Eval("VariantID") %>' class="btn btn--ghost btn--sm">
                                            Edit</a>
                                            <asp:LinkButton
                                                ID="btnChangeVariantStatus"
                                                runat="server"
                                                CommandName="ChangeStatus"
                                                CommandArgument='<%# Eval("VariantID") + "," + Eval("IsActive") %>'
                                                Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Deactivate" : "Reactivate" %>'
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
                <div style="margin-top:24px;">
                    <a href="ManagerProducts.aspx">Back to Product Management</a>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
