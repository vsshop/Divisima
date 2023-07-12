var authorize = new Vue({
    el: "#SignInSignUp",
    data: {
        show: false,
        menu: null
    },
    created() {
    },
    methods: {
        Show(menu = 'signin') {
            this.show = true;
            this.ToogleMenu(menu);
        },
        Hide() {
            this.show = false;
        },
        ToogleMenu(menu) {
            if (this.menu != menu) {
                this.menu = menu;
            }
        }
    }
})