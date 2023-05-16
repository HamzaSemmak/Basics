"use strict";
//["vjksdbvjsdbjk", "qdbjdbjkdbvsdjkvsdjk", "dvbdbdbdbjsdbjdjbn"]
const arr = (...params) => {
    params.forEach(element => {
        console.log(`str => ${element}, lenght => ${element.length}`);
    });
};
arr("hhhh", "yyyk", "trstdf", "hamza", "tariq");
