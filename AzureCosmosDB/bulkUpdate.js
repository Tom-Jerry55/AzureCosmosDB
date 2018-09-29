
//function bulkReplace(docs) {
//    if (!docs) {
//        throw new Error("Documents array is null or empty")
//    }
//    var collection = getContext().getCollection();
//    var collectionLink = collection.getSelfLink();
//    var response = getContext().getResponse();
//    var docCount = docs.length;

//    if (docCount === 0) {
//        response.setBody(0);
//        return;
//    }
//    var count = 0;
//    replaceDoc(docs[0]);

//    function replaceDoc(document) {
//       // var requestOptions = { etag: document._etag };
//        var isAccepted = collection.replaceDocument(document._self, document,
//            function callback(err, doc) {
//                if (err) {
//                    throw err;
//                }
//                count++;
//                if (count === docCount) {
//                    response.setBody(count);
//                }
//                else {
//                    replaceDoc(docs[count]);
//                }
//            }
//        );

//        if (!isAccepted) {
//            response.setBody(count);
//        }

//    }

//}

function bulkReplace(docs) {
    var collection = getContext().getCollection();
    var collectionLink = collection.getSelfLink();
    var count = 0;

    if (!docs) throw new Error("The array is undefined or null.");

    var docsLength = docs.length;
    if (docsLength === 0) {
        getContext().getResponse().setBody(0);
        return;
    }

    tryCreate(docs[count], callback);

    function tryCreate(doc, callback) {
        console.log("doc",doc);
        var isAccepted = collection.replaceDocument(doc._self, doc, callback);

        if (!isAccepted) getContext().getResponse().setBody(count);
    }

    function callback(err, doc, options) {
        if (err) throw err;

        count++;

        if (count >= docsLength) {

            getContext().getResponse().setBody(count);
        } else {

            tryCreate(docs[count], callback);
        }
    }
}



