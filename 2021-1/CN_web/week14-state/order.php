<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    <form action="order2.php" method = "post">
        <?php
            $hidden_field = $_POST['hidden_field'];
            $product = $_POST['product'];
            $quantity = $_POST['quantity'];
            print "<p style = 'color:red'>
                $hidden_field
            </p>";
            print "You selected product: " . $product . "and quantity: " . $quantity;
            print "<br><input type = 'hidden' name = 'product' value = '$product'";
            print "<br><input type = 'hidden' name = 'quantity' value = '$quantity'";
            print "<br>";
            print "Please enter your name: ";
            print "<input type = 'text' name = 'name'>";
            print "<br>";
            print "Enter your discount code:";
            print "<input type = 'text' name = 'code'";
            print "<br>";
            print "<input type = 'submit' value = 'Submit'>"
        ?>
    </form>
</body>
</html>