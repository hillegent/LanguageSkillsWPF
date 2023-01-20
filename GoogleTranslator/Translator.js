const translate = require('./get-word-info');

module.exports = (callback, word, orig, targ) => {

    translate(word, orig, targ, { examples: false }).then((res) => {
        callback(null, (JSON.stringify(res, undefined, 2)))
    }).catch(console.log);
}