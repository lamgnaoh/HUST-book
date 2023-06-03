<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <h1 style = "text-align:center">Success</h1>
    <?php
        $product = $_POST['product'];
        $quantity = $_POST['quantity'];
        print "Your payment to product: $product (quantity : $quantity) was successfully";
        print "<br>";
        print "<a href = 'index.html'style:'text-decoration: none; '>Return</a>"
    ?>
</body>
</html>