import Vue from "vue";
import { extend } from 'vee-validate';
import {
    ValidationProvider,
    ValidationObserver
} from 'vee-validate';
import {
    required,
    double,
    email,
    integer,
    max,
    max_value,
    min,
    min_value,
    numeric,
    oneOf,
    regex,
    required_if,
} from "vee-validate/dist/rules";

Vue.component('ValidationProvider', ValidationProvider);
Vue.component('ValidationObserver', ValidationObserver);

extend('required', required);
extend('email', email);
extend('integer', integer);
extend('max', max);
extend('max_value', max_value);
extend('min', min);
extend('min_value', min_value);
extend('numeric', numeric);
extend('oneOf', oneOf);
extend('regex', regex);
extend('required_if', required_if);
extend('double', double);

