""""
Name: Roderick Quiel
FSUID: riq19
Due Date: 04/25/2023
The program in this file is the individual work of Roderick Quiel
"""

a = int(input("Enter the value of a: "))
b = int(input("Enter the value of b: "))
p = int(input("Enter the value of p: "))

x = int(input("Enter the generator point x: "))
y = int(input("Enter the generator point y: "))

g = [x, y]


def slope(x1, x2):
    # Here we don't really check if P = Q. Instead, it looks like if k*G = infinity then the slope is undefined.
    if x1[0] - x2[0] == 0:
        return -1
    # Slope formula
    return (x1[1] - x2[1]) * pow((x1[0] - x2[0]), -1, p)


def dp_slope(x1):
    # slope if P = Q
    return (3*(x1[0]**2) + a) * pow(2*x1[1], -1, p)


def add_points(x1, x2):
    # Here we do check if P = Q
    if x1 == x2:
        return double_point(x1, x2)

    m = slope(x1, x2)
    # If the slope is undefined
    if m == -1:
        return -1

    x_s = (m**2 - x1[0] - x2[0]) % p
    y_s = -(x1[1] + m*(x_s - x1[0])) % p
    s = [x_s, y_s]
    return s


def double_point(x1, x2):
    # Formula if P = Q
    m = dp_slope(x1)
    x_s = (m**2 - x1[0] - x2[0]) % p
    y_s = -(x1[1] + m*(x_s - x1[0])) % p
    s = [x_s, y_s]
    return s


def scalar_point(n, x1):
    # 0 * G is undefined
    if n == 0:
        return "None"
    # 1 * G is just itself
    if n == 1:
        return x1
    s = []
    test = 0
    # here we start from 2 * G
    count = 2
    while count <= n:
        if len(s) == 0 or s == x1:
            s = add_points(x1, x1)
        else:
            s = add_points(s, x1)
            # We looped around so we start the cycle again
            # Technically we are skipping over the point k * G = infinity to  k + 1 * G = G or 1 * G.
            if s == -1:
                s = x1
                count += 1
        count += 1
    return s


secretkey_1 = int(input("Enter Ivan's private key: "))
secretkey_2 = int(input("Enter kelly's private key: "))

pubkey_1 = scalar_point(secretkey_1, g)
pubkey_2 = scalar_point(secretkey_2, g)


print("The public keys are: ")
print("Ivan: ", pubkey_1)
print("Kelly: ", pubkey_2)


agreedkey_1 = scalar_point(secretkey_1, pubkey_2)
agreedkey_2 = scalar_point(secretkey_2, pubkey_1)

print("The agreed upon keys: : ")
print("Ivan: ", agreedkey_1[0])
print("Kelly: ", agreedkey_2[0])














