//["vjksdbvjsdbjk", "qdbjdbjkdbvsdjkvsdjk", "dvbdbdbdbjsdbjdjbn"]
const arr = (...params: string[]): void => {
    params.forEach(element => {
        console.log(`str => ${element}, lenght => ${(element as string).length}`);
    });
}
arr("hhhh","yyyk","trstdf","hamza","tariq")
