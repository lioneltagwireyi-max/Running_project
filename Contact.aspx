<%@ Page Title="Contact PhoneFit" Language="C#" MasterPageFile="~/Phonefit.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="PhoneFit.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .contact-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 20px;
        }

        .contact-card {
            padding: 26px;
            background: var(--paper);
            border: 1px solid var(--rule);
            border-radius: var(--r);
        }

        .contact-card h2 {
            margin-bottom: 10px;
            font-size: 21px;
        }

        .contact-card p {
            color: var(--fg-mute);
            line-height: 1.6;
        }

        .contact-label {
            margin-bottom: 12px;
            font-family: var(--ff-mono);
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.07em;
            color: var(--indigo);
        }

        .contact-detail {
            display: block;
            margin: 16px 0;
            font-family: var(--ff-display);
            font-size: 18px;
            font-weight: 700;
            color: var(--ink);
        }

        .contact-hours {
            margin-top: 12px;
            padding-top: 12px;
            border-top: 1px solid var(--rule);
            font-size: 13px;
            color: var(--fg-mute);
        }

        .contact-actions {
            display: flex;
            gap: 12px;
            flex-wrap: wrap;
            margin-top: 28px;
        }

        @media (max-width: 850px) {
            .contact-grid {
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
                    <a href="Home.aspx">Home</a>
                    <span class="sep">›</span>
                    <span>Contact</span>
                </div>
                <h1>Contact PhoneFit</h1>
                <p>
                    Need help with your account, an order,
                    or choosing a smartphone? Contact PhoneFit
                    using one of the options below.
                </p>
            </div>
        </section>

        <section class="section">
            <div class="container">
                <div class="contact-grid">
                    <article class="contact-card">
                        <div class="contact-label">
                            Email Support
                        </div>
                        <h2>Email us</h2>
                        <p>
                            Contact us for account,
                            product, order or invoice assistance.
                        </p>
                        <a href="mailto:support@phonefit.co.za"
                           class="contact-detail">
                            support@phonefit.co.za
                        </a>
                    </article>
                    <article class="contact-card">
                        <div class="contact-label">
                            Phone Support
                        </div>
                        <h2>Call us</h2>
                        <p>
                            Speak to PhoneFit support for 
                            assistance with your shopping experience.
                        </p>
                        <a href="tel:+27115550147"
                           class="contact-detail">
                            +27 11 555 0147
                        </a>
                    </article>
                    <article class="contact-card">
                        <div class="contact-label">
                            Orders &amp; Invoices
                        </div>
                        <h2>Previous purchases</h2>
                        <p>
                            Logged in customers can review previous
                            orders and access their invoices
                            through My Orders.
                        </p>
                        <a href="MyOrders.aspx"
                           class="btn btn--ghost">
                            View My Orders
                        </a>
                    </article>
                </div>
                <div class="contact-actions">

                    <a href="shop.aspx"
                       class="btn btn--indigo">
                        Shop Phones
                    </a>

                    <a href="cart.aspx"
                       class="btn btn--ghost">
                        View Cart
                    </a>
                </div>
            </div>
        </section>
    </main>
</asp:Content>
