var header = new Vue({
    el: "#header",
    data: {
        cart: 0
    },
    created() {
    },
    methods: {
        SignIn() {
            authorize.Show('signin');
        },
        SignUp() {
            authorize.Show('signup');
        },
        CartUpdate(count) {
            this.cart = count;
        }
    }
})