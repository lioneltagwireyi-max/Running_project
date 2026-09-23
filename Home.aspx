<%@ Page Title="Home" Language="C#" MasterPageFile="~/Phonefit.Master"
    AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PhoneFit.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>

        .compact-row {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 20px;
        }

        .compact-card {
            display: block;
            width: 100%;
            min-width: 0;
            padding: 24px;
        }

        .compact-card > div {
            display: block;
            width: 100%;
        }

        .compact-card .stock {
            margin-bottom: 8px;
        }

        .compact-card .name {
            display: block;
            margin-bottom: 12px;
            line-height: 1.3;
        }

        .compact-card p {
            width: 100%;
            margin-bottom: 18px;
            line-height: 1.6;
        }

        @media (max-width: 950px) {

            .compact-row {
                grid-template-columns: repeat(2, 1fr);
            }

        }

        @media (max-width: 600px) {

            .compact-row {
                grid-template-columns: 1fr;
            }

        }

    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <!-- HERO -->
        <section class="hero">
            <div class="container">
                <div class="bento">

                    <article class="bento-card bento-card--lg">
                        <div class="sparkle"></div>

                        <div>
                            <span class="eyebrow">
                                SMARTPHONE SHOPPING MADE SIMPLE
                            </span>

                            <h2>
                                Find the right phone<br />
                                for your needs
                            </h2>

                            <p>
                                Explore available smartphones, review detailed
                                specifications and choose the variant that fits
                                your needs and budget.
                            </p>

                            <a href="shop.aspx" class="btn btn--paper">
                                Shop phones →
                            </a>
                        </div>
                    </article>


                    <article class="bento-card bento-card--purple">
                        <div class="sparkle"></div>

                        <span class="eyebrow">
                            PHONE CATALOGUE
                        </span>

                        <h3 style="font-size: var(--text-xl); line-height: 1.15;">
                            Browse available<br />
                            smartphone brands
                        </h3>

                        <a href="shop.aspx" class="shop-now">
                            View catalogue →
                        </a>
                    </article>


                    <article class="bento-card bento-card--teal">
                        <div class="sparkle"></div>

                        <span class="eyebrow">
                            PRODUCT DETAILS
                        </span>

                        <h3 style="font-size: var(--text-xl); line-height: 1.15;">
                            Review detailed<br />
                            specifications
                        </h3>

                        <a href="shop.aspx" class="shop-now">
                            Explore phones →
                        </a>
                    </article>


                    <div class="bento-row">

                        <article class="bento-card bento-card--orange">
                            <div class="sparkle"></div>

                            <span class="eyebrow">
                                COMPARE PHONES
                            </span>

                            <h3 style="font-size: var(--text-lg); line-height: 1.2;">
                                Compare smartphone<br />
                                specifications
                            </h3>

                            <a href="ComparePhones.aspx" class="shop-now">
                                Compare phones →
                            </a>
                        </article>

                        <article class="bento-card bento-card--green">
                            <div class="sparkle"></div>

                            <span class="eyebrow">
                                VARIANTS
                            </span>

                            <h3 style="font-size: var(--text-lg); line-height: 1.2;">
                                Select RAM,<br />
                                storage and colour
                            </h3>

                            <a href="shop.aspx" class="shop-now">
                                Browse variants →
                            </a>
                        </article>


                        <article class="bento-card bento-card--black">
                            <div class="sparkle"></div>

                            <span class="eyebrow">
                                SHOPPING CART
                            </span>

                            <h3 style="font-size: var(--text-lg); line-height: 1.2;">
                                Manage your selected<br />
                                phone variants
                            </h3>

                            <a href="cart.aspx" class="shop-now">
                                View cart →
                            </a>
                        </article>

                    </div>
                </div>
            </div>
        </section>


        <!-- FEATURED PHONES -->
        <section class="section" style="padding-top: var(--s5);">
            <div class="container">

                <div class="section-head">
                    <h2>Featured phones</h2>

                    <a href="shop.aspx" class="view-all">
                        View all phones →
                    </a>
                </div>


                <asp:Panel ID="pnlNoPhones" runat="server" Visible="false">
                    <div style="padding: 40px; text-align: center; background: var(--paper); border-radius: var(--r);">
                        <h3>No phones are currently available</h3>
                        <p>Please check the catalogue again later.</p>
                    </div>
                </asp:Panel>


                <div class="products">
                    <asp:Repeater ID="rptFeaturedPhones" runat="server">
                        <ItemTemplate>

                            <article class="product-card">

                                <div class="img-wrap">
                                    <span class="badge">Featured</span>

                                    <asp:Image
                                        ID="imgPhone"
                                        runat="server"
                                        ImageUrl='<%# Eval("ImagePath") %>'
                                        AlternateText='<%# Eval("ModelName") %>' />
                                </div>


                                <div class="stock">
                                    <span class="dot"></span>
                                    In stock · <%# Eval("StockQuantity") %> items
                                </div>

                               <asp:HyperLink
                                    ID="lnkPhoneName"
                                    runat="server"
                                    CssClass="name"
                                    NavigateUrl='<%# "product.aspx?id=" + Eval("PhoneModelID") %>'
                                    Text='<%# Eval("BrandName") + " " + Eval("ModelName") %>'>
                                </asp:HyperLink>

                                <div class="price">
                                    <span class="now">
                                        From R<%# Eval("StartingPrice", "{0:N2}") %>
                                    </span>
                                </div>


                                <asp:HyperLink
                                    ID="lnkViewPhone"
                                    runat="server"
                                    CssClass="btn"
                                    NavigateUrl='<%# "product.aspx?id=" + Eval("PhoneModelID") %>'>

                                    View phone →

                                </asp:HyperLink>

                            </article>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </section>


        <!-- WHY PHONEFIT -->
        <section class="section" style="padding-top: 0;">
            <div class="container">

                <div class="section-head">
                    <h2>Why choose PhoneFit?</h2>
                </div>

                <div class="compact-row">

                    <article class="compact-card">
                        <div>
                            <div class="stock">DETAILED INFORMATION</div>
                            <div class="name">Review specifications</div>

                            <p>
                                View processor, display, battery, camera,
                                connectivity and storage information before buying.
                            </p>

                            <a href="shop.aspx" class="btn">
                                Browse phones
                            </a>
                        </div>
                    </article>


                    <article class="compact-card">
                        <div>
                            <div class="stock">MULTIPLE OPTIONS</div>
                            <div class="name">Choose phone variants</div>

                            <p>
                                Select from available RAM, storage and colour
                                configurations for supported smartphone models.
                            </p>

                            <a href="shop.aspx" class="btn">
                                View options
                            </a>
                        </div>
                    </article>


                    <article class="compact-card">
                        <div>
                            <div class="stock">SECURE CHECKOUT</div>
                            <div class="name">Complete your order</div>

                            <p>
                                Manage cart quantities, review transaction totals
                                and complete your order through checkout.
                            </p>

                            <a href="cart.aspx" class="btn">
                                View cart
                            </a>
                        </div>
                    </article>


                    <article class="compact-card">
                        <div>
                            <div class="stock">ORDER HISTORY</div>
                            <div class="name">Access previous invoices</div>

                            <p>
                                Logged in customers can review previous purchases
                                and view invoices from My Orders.
                            </p>

                            <a href="MyOrders.aspx" class="btn">
                                My Orders
                            </a>
                        </div>
                    </article>

                </div>
            </div>
        </section>


        <!-- CALL TO ACTION -->
        <section style="background: var(--paper);">
            <div class="container">

                <div class="newsletter">
                    <div class="newsletter-grid">

                        <div>
                            <h2>Ready to find your next smartphone?</h2>

                            <p>
                                Browse PhoneFit's catalogue, review specifications,
                                choose an available variant and securely complete
                                your order.
                            </p>
                        </div>


                        <div style="display: flex; align-items: center; justify-content: flex-end; gap: 12px; flex-wrap: wrap;">

                            <a href="shop.aspx" class="btn btn--indigo">
                                Shop phones →
                            </a>

                        </div>

                    </div>
                </div>

            </div>
        </section>

    </main>


</asp:Content>
