<%@ Page Title="Product Management" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerProducts.aspx.cs" Inherits="PhoneFit.ManagerProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

    .manager-product-table {
        width: 100%;
        border-collapse: collapse;
        background: var(--paper);
        border: 1px solid var(--rule);
    }

    .manager-product-table th,
    .manager-product-table td {
        padding: 14px;
        text-align: left;
        border-bottom: 1px solid var(--rule);
    }

    .manager-product-table th {
        background: var(--bg);
        font-family: var(--ff-mono);
        font-size: 11px;
        text-transform: uppercase;
        letter-spacing: 0.06em;
        color: var(--fg-mute);
    }

    .manager-product-table td {
        font-size: 14px;
        color: var(--fg-soft);
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

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main id="main">

    <section class="page-head">
        <div class="container">
            <div class="crumbs">
                <a href="Manager.aspx">Manager</a>
                <span class="sep">›</span>
                <span>Product Management</span>
            </div>
            <h1>Product Management</h1>
            <p>View and manage the smartphones available in the PhoneFit catalogue.</p>
        </div>
    </section>

    <section class="section">
        <div class="container">
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div style="margin-bottom: 20px;">
                <a href="ManagerAddProduct.aspx" class="btn btn--indigo">
                    Add Product
                </a>
            </div>
            <asp:Panel ID="pnlNoProducts" runat="server" Visible="false">
                <div style=" padding: 40px; text-align: center; background: var(--bg); border-radius: var(--r);">
                    No products were found.
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlProducts" runat="server">
                <div style="overflow-x: auto;">
                    <table class="manager-product-table">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Brand</th>
                                <th>Model</th>
                                <th>Operating System</th>
                                <th>Release Year</th>
                                <th>Variants</th>
                                <th>Total Stock</th>
                                <th>Status</th>
                                <th>Date Added</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptProducts" runat="server" OnItemCommand="rptProducts_ItemCommand">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("PhoneModelID") %></td>
                                        <td><%# Eval("BrandName") %></td>
                                        <td><%# Eval("ModelName") %></td>
                                        <td><%# Eval("OperatingSystem") %></td>
                                        <td><%# Eval("ReleaseYear") %></td>
                                        <td><%# Eval("VariantCount") %></td>
                                        <td><%# Eval("TotalStock") %></td>
                                        <td><asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "Inactive" %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "status-active" : "status-inactive" %>'>
                                        </asp:Label></td>
                                        <td><%# Eval("DateAdded", "{0:dd MMM yyyy}") %></td>
                                        <td> <a href='<%# "ManagerEditProduct.aspx?id=" + Eval("PhoneModelID") %>' class="btn btn--ghost btn--sm">Edit</a>

                                        <a href='<%# "ManagerVariants.aspx?id=" + Eval("PhoneModelID") %>' class="btn btn--ghost btn--sm">
                                        Manage Variants</a>

                                        <asp:LinkButton ID="btnChangeStatus" runat="server" CommandName="ChangeStatus" CommandArgument='<%# Eval("PhoneModelID") + "," + Eval("IsActive") %>'
                                        Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Deactivate" : "Reactivate" %>' CssClass="btn btn--ghost btn--sm"
                                        CausesValidation="false"></asp:LinkButton></td>
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
