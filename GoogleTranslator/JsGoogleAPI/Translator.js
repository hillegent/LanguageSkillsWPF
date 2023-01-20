const translate = require('./get-word-info');

key = 'AIzaSyA_PX6UNG1laDJquPfMBvVYxBMtdwL00cc';
const fs = require('fs');

module.exports = (callback, word, orig, targ) => {

    translate(word, orig, targ, { examples: false }).then((res) => {
        callback(null, (JSON.stringify(res, undefined, 2)) )
    }).catch(console.log);
}

//fs.writeFile('C:/Users/Home/source/txt/test.txt', (JSON.stringify(res, undefined, 2)), err => {
//    if (err) {
//        console.error(err);
//    }
//    // file written successfully
//});