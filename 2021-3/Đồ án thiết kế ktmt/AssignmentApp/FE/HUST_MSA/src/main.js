import { createApp } from 'vue'
import App from './App.vue'
import PrimeVue from 'primevue/config';
import router from './router'
import ConfirmationService from 'primevue/confirmationservice';
import ToastService from 'primevue/toastservice';
import 'primevue/resources/themes/saga-blue/theme.css'       //theme
import 'primevue/resources/primevue.min.css'                 //core css
import 'primeicons/primeicons.css'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { fab } from '@fortawesome/free-brands-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import mitt from "mitt";
const emitter = mitt();
library.add(fas, fab, far)
const app = createApp(App);
app.use(router);
app.use(PrimeVue);
app.use(ConfirmationService);
app.use(ToastService );
app.component('font-awesome-icon', FontAwesomeIcon)
app.config.globalProperties.emitter = emitter;
app.mount('#app')  