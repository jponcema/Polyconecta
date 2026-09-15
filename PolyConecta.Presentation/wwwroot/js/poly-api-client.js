/**
 * PolyConecta JavaScript Client SDK
 * Unified REST API Communication Layer for Odoo 19 Web SPA
 */
(function (global) {
    'use strict';

    class PolyApiClient {
        constructor(baseUrl = '') {
            if (!baseUrl && typeof window !== 'undefined') {
                if (window.location.port !== '9020') {
                    baseUrl = 'http://localhost:9020';
                }
            }
            this.baseUrl = baseUrl;
        }

        async request(endpoint, method = 'GET', body = null, headers = {}) {
            const url = `${this.baseUrl}${endpoint}`;
            const defaultHeaders = {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                'X-Requested-With': 'PolyConecta-Odoo19-SPA'
            };

            const options = {
                method,
                headers: { ...defaultHeaders, ...headers }
            };

            if (body && (method === 'POST' || method === 'PUT' || method === 'PATCH')) {
                options.body = JSON.stringify(body);
            }

            try {
                const response = await fetch(url, options);
                const data = await response.json();

                if (!response.ok) {
                    const errorMsg = data.detail || data.title || response.statusText || 'API Error';
                    throw new Error(`[${response.status}] ${errorMsg}`);
                }

                return data;
            } catch (err) {
                console.error(`[PolyAPI.client] Request failed [${method} ${endpoint}]:`, err);
                throw err;
            }
        }

        async get(endpoint, headers = {}) {
            return this.request(endpoint, 'GET', null, headers);
        }

        async post(endpoint, body, headers = {}) {
            return this.request(endpoint, 'POST', body, headers);
        }

        async put(endpoint, body, headers = {}) {
            return this.request(endpoint, 'PUT', body, headers);
        }

        async delete(endpoint, headers = {}) {
            return this.request(endpoint, 'DELETE', null, headers);
        }
    }

    global.PolyAPI = global.PolyAPI || {};
    global.PolyAPI.client = new PolyApiClient();
})(typeof window !== 'undefined' ? window : this);
