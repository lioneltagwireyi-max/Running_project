<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="PhoneFit.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .checkout-layout {
            display: grid;
            grid-template-columns: 1fr 340px;
            gap: 28px;
            align-items: start;
        }

        .checkout-items,
        .checkout-summary {
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .checkout-items {
            overflow: hidden;
        }

        .checkout-row {
            display: grid;
            grid-template-columns: 1fr 180px 90px 140px;
            gap: 16px;
            align-items: center;
            padding: 18px;
            border-bottom: 1px solid var(--rule);
        }

        .checkout-row:last-child {
            border-bottom: 0;
        }

        .checkout-header {
            background: var(--bg);
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.06em;
            color: var(--fg-mute);
        }

        .checkout-product-name {
            font-weight: 700;
            color: var(--ink);
        }

        .checkout-variant {
            margin-top: 4px;
            font-size: 13px;
            color: var(--fg-mute);
        }

        .checkout-summary {
            padding: 22px;
        }

        .checkout-summary h2 {
            margin-bottom: 20px;
        }

        .summary-line {
            display: flex;
            justify-content: space-between;
            gap: 20px;
            padding: 10px 0;
            border-bottom: 1px solid var(--rule);
        }

        .summary-line strong {
            color: var(--ink);
        }

        .summary-total {
            font-size: 20px;
            font-weight: 700;
        }

        .checkout-actions {
            margin-top: 20px;
        }

        @media (max-width: 850px) {
            .checkout-layout {
                grid-template-columns: 1fr;
            }

            .checkout-row {
                grid-template-columns: 1fr;
            }

            .checkout-header {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <section class="page-head">
            <div class="container">
                <div class="crumbs">
                    <a href="cart.aspx">Cart</a>
                    <span class="sep">›</span>
                    <span>Checkout</span>
                </div>

                <h1>Checkout</h1>
                <p>Review the items in your cart before placing your order.</p>
            </div>
        </section>

        <section class="section">
            <div class="container">

                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                <asp:Panel ID="pnlEmptyCart" runat="server" Visible="false">
                    <div style="padding:40px; text-align:center; background:var(--paper); border:1px solid var(--rule); border-radius:var(--r);">
                        <h2>Your cart is empty</h2>
                        <p>Add a smartphone to your cart before proceeding to checkout.</p>
                        <a href="shop.aspx" class="btn btn--indigo">Shop Phones</a>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlCheckout" runat="server" Visible="false">

                    <div class="checkout-layout">

                        <div class="checkout-items">

                            <div class="checkout-row checkout-header">
                                <div>Product</div>
                                <div>Unit Price</div>
                                <div>Quantity</div>
                                <div>Line Total</div>
                            </div>

                            <asp:Repeater ID="rptCheckoutItems" runat="server">
                                <ItemTemplate>
                                    <div class="checkout-row">

                                        <div>
                                            <div class="checkout-product-name">
                                                <%# Eval("ModelName") %>
                                            </div>

                                            <div class="checkout-variant">
                                                <%# Eval("VariantDescription") %>
                                            </div>
                                        </div>

                                        <div>
                                            R<%# Eval("UnitPrice", "{0:N2}") %>
                                        </div>

                                        <div>
                                            <%# Eval("Quantity") %>
                                        </div>

                                        <div>
                                            R<%# Eval("LineTotal", "{0:N2}") %>
                                        </div>

                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>

                        <aside class="checkout-summary">
                            <h2>Order Summary</h2>

                            <div class="summary-line">
                                <span>Items</span>
                                <asp:Label ID="lblItemCount" runat="server"></asp:Label>
                            </div>

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
                                <strong>Total</strong>
                                <strong>
                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                </strong>
                            </div>

                            <div class="checkout-actions">
                                <asp:Button
                                    ID="btnPlaceOrder"
                                    runat="server"
                                    Text="Place Order"
                                    CssClass="btn btn--indigo btn--block"
                                    OnClick="btnPlaceOrder_Click"
                                    OnClientClick="return confirm('Are you sure you want to place this order?');" />

                                <a href="cart.aspx" class="btn btn--ghost" style="margin-top: 10px;">
                                    Back to Cart
                                </a>
                            </div>
                        </aside>
                    </div>
                </asp:Panel>
            </div>
        </section>
    </main>
</asp:Content>
