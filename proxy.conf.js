const { env } = require('process');

const target = 'http://localhost:7071';

const PROXY_CONFIG = [
  {
    context: ['/api'],
    target: target,
    secure: false,
    changeOrigin: true,
    logLevel: 'debug',
  },
];

module.exports = PROXY_CONFIG;
