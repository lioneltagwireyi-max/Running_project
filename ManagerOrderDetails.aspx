<%@ Page Title="Transaction Details" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="ManagerOrderDetails.aspx.cs" Inherits="PhoneFit.ManagerOrderDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .transaction-card {
            max-width: 980px;
            margin: 0 auto;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
            overflow: hidden;
        }

        .transaction-head {
            display: flex;
            justify-content: space-between;
            gap: 24px;
            padding: 28px;
            border-bottom: 1px solid var(--rule);
            background: var(--bg);
        }

        .transaction-meta {
            text-align: right;
            font-size: 14px;
            line-height: 1.7;
        }

        .customer-info {
            padding: 24px 28px;
            border-bottom: 1px solid var(--rule);
        }

        .section-label {
            margin-bottom: 8px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--fg-mute);
        }

        .transaction-table-wrapper {
            overflow-x: auto;
        }

        .transaction-table {
            width: 100%;
            border-collapse: collapse;
        }

        .transaction-table th,
        .transaction-table td {
            padding: 15px 18px;
            text-align: left;
            border-bottom: 1px solid var(--rule);
        }

        .transaction-table th {
            background: var(--bg);
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .product-name {
            font-weight: 700;
            color: var(--ink);
        }

        .variant-description {
            margin-top: 4px;
            font-size: 13px;
            color: var(--fg-mute);
        }

        .transaction-summary {
            width: 420px;
            margin-left: auto;
            margin-right: 90px;
            padding: 24px 28px 28px;
        }

        .summary-line {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            padding: 9px 0;
            border-bottom: 1px solid var(--rule);
        }

        .summary-total {
            font-size: 20px;
            font-weight: 700;
            color: var(--ink);
        }

        .status-badge {
            display: inline-block;
            padding: 5px 10px;
            border-radius: 999px;
            background: #dff7eb;
            color: #087443;
            font-size: 12px;
            font-weight: 600;
        }

        .transaction-actions {
            max-width: 980px;
            margin: 22px auto 0;
        }

        @media (max-width: 700px) {
            .transaction-head {
                flex-direction: column;
            }

            .transaction-meta {
                text-align: left;
            }

            .transaction-summary {
                width: auto;
                margin-right: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="ManagerOrders.aspx">Orders & Transactions</a>
                    <span class="sep">›</span>
                    <span>Transaction Details</span>
                </div>

                <h1>Transaction Details</h1>
                <p>View the complete details of the selected customer order.</p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <asp:Panel ID="pnlTransaction" runat="server" Visible="false">

                    <div class="transaction-card">

                        <div class="transaction-head">
                            <div>
                                <h2>PhoneFit Transaction</h2>
                                <div>
                                    Order #<asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="transaction-meta">
                                <div>
                                    <strong>Date:</strong>
                                    <asp:Label ID="lblOrderDate" runat="server"></asp:Label>
                                </div>

                                <div>
                                    <strong>Status:</strong>
                                    <asp:Label ID="lblOrderStatus" runat="server" CssClass="status-badge"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="customer-info">
                            <div class="section-label">Customer</div>

                            <div>
                                <strong>
                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                </strong>
                            </div>

                            <div>
                                <asp:Label ID="lblCustomerEmail" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="transaction-table-wrapper">
                            <table class="transaction-table">
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Unit Price</th>
                                        <th>Quantity</th>
                                        <th>Line Total</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Repeater ID="rptOrderItems" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div class="product-name">
                                                        <%# Eval("ModelName") %>
                                                    </div>

                                                    <div class="variant-description">
                                                        <%# Eval("VariantDescription") %>
                                                    </div>
                                                </td>

                                                <td>
                                                    R<%# Eval("UnitPrice", "{0:N2}") %>
                                                </td>

                                                <td>
                                                    <%# Eval("Quantity") %>
                                                </td>

                                                <td>
                                                    R<%# Eval("LineTotal", "{0:N2}") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>

                        <div class="transaction-summary">
                            <div class="summary-line">
                                <span>Subtotal</span>
                                <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                            </div>

                            <div class="summary-line">
                                <span>Discount</span>
                                <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                            </div>

                            <div class="summary-line">
                                <span>Shipping</span>
                                <asp:Label ID="lblShipping" runat="server"></asp:Label>
                            </div>

                            <div class="summary-line">
                                <span>VAT included</span>
                                <asp:Label ID="lblVAT" runat="server"></asp:Label>
                            </div>

                            <div class="summary-line summary-total">
                                <span>Total</span>
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </div>
                        </div>

                    </div>

                    <div class="transaction-actions">
                        <a href="ManagerOrders.aspx" class="btn btn--ghost">Back to Orders</a>
                    </div>

                </asp:Panel>

            </div>
        </section>

    </main>

</asp:Content>
