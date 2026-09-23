<%@ Page Title="" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="PhoneFit.Invoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .invoice-card {
            max-width: 980px;
            margin: 0 auto;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
            overflow: hidden;
        }

        .invoice-head {
            display: flex;
            justify-content: space-between;
            gap: 24px;
            padding: 28px;
            border-bottom: 1px solid var(--rule);
            background: var(--bg);
        }

        .invoice-head h2 {
            margin-bottom: 8px;
        }

        .invoice-meta {
            text-align: right;
            font-size: 14px;
            line-height: 1.7;
        }

        .invoice-customer {
            padding: 24px 28px;
            border-bottom: 1px solid var(--rule);
        }

        .invoice-customer .label {
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--fg-mute);
            margin-bottom: 8px;
        }

        .invoice-table-wrapper {
            overflow-x: auto;
        }

        .invoice-table {
            width: 100%;
            border-collapse: collapse;
        }

        .invoice-table th,
        .invoice-table td {
            padding: 15px 18px;
            text-align: left;
            border-bottom: 1px solid var(--rule);
        }

        .invoice-table th {
            background: var(--bg);
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .invoice-product {
            font-weight: 700;
            color: var(--ink);
        }

        .invoice-variant {
            margin-top: 4px;
            font-size: 13px;
            color: var(--fg-mute);
        }

        .invoice-summary {
            width: 420px;
            margin-left: auto;
            margin-right: 90px;
            padding: 24px 28px 28px;
        }

        .invoice-line {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            padding: 9px 0;
            border-bottom: 1px solid var(--rule);
        }

        .invoice-total {
            font-size: 20px;
            font-weight: 700;
            color: var(--ink);
        }

        .invoice-actions {
            max-width: 980px;
            margin: 22px auto 0;
            display: flex;
            gap: 12px;
            flex-wrap: wrap;
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

        @media (max-width: 700px) {
            .invoice-head {
                flex-direction: column;
            }

            .invoice-meta {
                text-align: left;
            }

            .invoice-summary {
                width: auto;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="Home.aspx">Home</a>
                    <span class="sep">›</span>
                    <span>Invoice</span>
                </div>

                <h1>Invoice</h1>
                <p>View the details of your completed PhoneFit transaction.</p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <asp:Panel ID="pnlInvoice" runat="server" Visible="false">

                    <div class="invoice-card">

                        <div class="invoice-head">
                            <div>
                                <h2>PhoneFit Invoice</h2>
                                <div>
                                    Order #<asp:Label ID="lblOrderID" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="invoice-meta">
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

                        <div class="invoice-customer">
                            <div class="label">Billed To</div>

                            <div>
                                <strong>
                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                </strong>
                            </div>

                            <div>
                                <asp:Label ID="lblCustomerEmail" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="invoice-table-wrapper">
                            <table class="invoice-table">
                                <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th>Unit Price</th>
                                        <th>Quantity</th>
                                        <th>Line Total</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Repeater ID="rptInvoiceItems" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <div class="invoice-product">
                                                        <%# Eval("ModelName") %>
                                                    </div>

                                                    <div class="invoice-variant">
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

                        <div class="invoice-summary">

                            <div class="invoice-line">
                                <span>Subtotal</span>
                                <asp:Label ID="lblSubtotal" runat="server"></asp:Label>
                            </div>

                            <div class="invoice-line">
                                <span>Discount</span>
                                <asp:Label ID="lblDiscount" runat="server"></asp:Label>
                            </div>

                            <div class="invoice-line">
                                <span>Shipping</span>
                                <asp:Label ID="lblShipping" runat="server"></asp:Label>
                            </div>

                            <div class="invoice-line">
                                <span>VAT included</span>
                                <asp:Label ID="lblVAT" runat="server"></asp:Label>
                            </div>

                            <div class="invoice-line invoice-total">
                                <span>Total</span>
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="invoice-actions">
                        <a href="shop.aspx" class="btn btn--indigo">Continue Shopping</a>
                    </div>
                </asp:Panel>
            </div>
        </section>
    </main>
</asp:Content>
