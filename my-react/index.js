const express = require('express');
const app = express();
var path = require('path');
app.set('views', path.join(__dirname, 'pages'));
app.set('view engine', 'hbs');

app.get('/', async function(req, res, next) {
    res.render('index')
});

app.listen(3000, () => console.log('Example app listening on port 3000!'))
