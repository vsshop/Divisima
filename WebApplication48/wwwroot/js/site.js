var app = new Vue({
    el: "#app",
    data: {
        load: false,
        products: [],
        cart: [],
        toast: null,
        product: null
    },
    created() {
        this.Init();
    },
    mounted() {
        let options = {
            animation: true,
            autohide: true,
            delay: 3000
        }
        this.toast = new bootstrap.Toast(this.$refs.toast, options);
    },
    computed: {
        sum() {
            return this.cart.map(p => p.price).reduce((s, a) => s + a);
        }
    },
    methods: {
        async Init() {
            let resultCart = await axios.get("/ApiUser/GetCart");
            this.cart = resultCart.data;
            header.CartUpdate(this.cart.length);

            let resultProduct = await axios.get("/ApiProduct/GetProducts");
            this.products = resultProduct.data;


            this.load = true;
        },
        async AddProduct(product) {
            let resultCart = await axios.post("/ApiUser/AppendProductToCart", product);
            this.cart = resultCart.data;
            header.CartUpdate(this.cart.length);
            this.toast.show();
            this.product = product;
        }
    }
})