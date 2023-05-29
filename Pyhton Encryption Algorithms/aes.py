""""
Name: Roderick Quiel
FSUID: riq19
Due Date: 04/25/2023
The program in this file is the individual work of Roderick Quiel
"""

S_BOX = [
    [0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76],
    [0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0],
    [0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15],
    [0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75],
    [0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84],
    [0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF],
    [0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8],
    [0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2],
    [0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73],
    [0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB],
    [0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79],
    [0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08],
    [0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A],
    [0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E],
    [0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF],
    [0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16]
]

InS_BOX = [
    [0x52, 0x09, 0x6A, 0xD5, 0x30, 0x36, 0xA5, 0x38, 0xBF, 0x40, 0xA3, 0x9E, 0x81, 0xF3, 0xD7, 0xFB],
    [0x7C, 0xE3, 0x39, 0x82, 0x9B, 0x2F, 0xFF, 0x87, 0x34, 0x8E, 0x43, 0x44, 0xC4, 0xDE, 0xE9, 0xCB],
    [0x54, 0x7B, 0x94, 0x32, 0xA6, 0xC2, 0x23, 0x3D, 0xEE, 0x4C, 0x95, 0x0B, 0x42, 0xFA, 0xC3, 0x4E],
    [0x08, 0x2E, 0xA1, 0x66, 0x28, 0xD9, 0x24, 0xB2, 0x76, 0x5B, 0xA2, 0x49, 0x6D, 0x8B, 0xD1, 0x25],
    [0x72, 0xF8, 0xF6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xD4, 0xA4, 0x5C, 0xCC, 0x5D, 0x65, 0xB6, 0x92],
    [0x6C, 0x70, 0x48, 0x50, 0xFD, 0xED, 0xB9, 0xDA, 0x5E, 0x15, 0x46, 0x57, 0xA7, 0x8D, 0x9D, 0x84],
    [0x90, 0xD8, 0xAB, 0x00, 0x8C, 0xBC, 0xD3, 0x0A, 0xF7, 0xE4, 0x58, 0x05, 0xB8, 0xB3, 0x45, 0x06],
    [0xD0, 0x2C, 0x1E, 0x8F, 0xCA, 0x3F, 0x0F, 0x02, 0xC1, 0xAF, 0xBD, 0x03, 0x01, 0x13, 0x8A, 0x6B],
    [0x3A, 0x91, 0x11, 0x41, 0x4F, 0x67, 0xDC, 0xEA, 0x97, 0xF2, 0xCF, 0xCE, 0xF0, 0xB4, 0xE6, 0x73],
    [0x96, 0xAC, 0x74, 0x22, 0xE7, 0xAD, 0x35, 0x85, 0xE2, 0xF9, 0x37, 0xE8, 0x1C, 0x75, 0xDF, 0x6E],
    [0x47, 0xF1, 0x1A, 0x71, 0x1D, 0x29, 0xC5, 0x89, 0x6F, 0xB7, 0x62, 0x0E, 0xAA, 0x18, 0xBE, 0x1B],
    [0xFC, 0x56, 0x3E, 0x4B, 0xC6, 0xD2, 0x79, 0x20, 0x9A, 0xDB, 0xC0, 0xFE, 0x78, 0xCD, 0x5A, 0xF4],
    [0x1F, 0xDD, 0xA8, 0x33, 0x88, 0x07, 0xC7, 0x31, 0xB1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xEC, 0x5F],
    [0x60, 0x51, 0x7F, 0xA9, 0x19, 0xB5, 0x4A, 0x0D, 0x2D, 0xE5, 0x7A, 0x9F, 0x93, 0xC9, 0x9C, 0xEF],
    [0xA0, 0xE0, 0x3B, 0x4D, 0xAE, 0x2A, 0xF5, 0xB0, 0xC8, 0xEB, 0xBB, 0x3C, 0x83, 0x53, 0x99, 0x61],
    [0x17, 0x2B, 0x04, 0x7E, 0xBA, 0x77, 0xD6, 0x26, 0xE1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0C, 0x7D]
]

mix = [[2, 3, 1, 1], [1, 2, 3, 1], [1, 1, 2, 3], [3, 1, 1, 2]]

inv_mix = [[0x0E, 0x0B, 0x0D, 0x09],[0x09, 0x0E, 0x0B, 0x0D], [0x0D, 0x09, 0x0E, 0x0B], [0x0B, 0x0D, 0x09, 0x0E]]

round_const = [0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36]

# Helper function for the imix_cols
def mult_by_2(num):

    bit1 = bin(int(num, 16))
    # All this performs hex multiplication by 2
    # The ifs statements are to deal with some shenanigans with bin() function
    if len(bit1[2:]) < 8:
        # If the leading bit is 1 then after the shift we xor with 0x1b or 27
        if int(bit1[0]) == 1:
            bit1 = bin(int(num, 16) << 1)
            # This deals with the semantics of bin() function
            if len(bit1[2:]) > 8:
                bit1 = (int(bit1[3:], 2) ^ 27)
            else:
                bit1 = (int(bit1, 2) ^ 27)
            return hex(bit1)
        else:
            bit1 = bin(int(num, 16) << 1)
            if len(bit1[2:]) > 8:
                bit1 = (int(bit1[3:], 2))
            else:
                bit1 = (int(bit1, 2))
            return hex(bit1)
    else:
        if int(bit1[2]) == 1:
            bit1 = bin(int(num, 16) << 1)
            if len(bit1[2:]) > 8:
                bit1 = (int(bit1[3:], 2) ^ 27)
            else:
                bit1 = (int(bit1, 2) ^ 27)
            return hex(bit1)
        else:
            bit1 = bin(int(num, 16) << 1)
            if len(bit1[2:]) > 8:
                bit1 = (int(bit1[3:], 2))
            else:
                bit1 = (int(bit1, 2))
            return hex(bit1)

# This is jus xor two matrices. It is the add round function
def ark(h_list, k_list):
    to_return = []
    for i in range(0, 16):
        to_return.append(hex(int(h_list[i], 16) ^ int(k_list[i], 16)))
    return to_return

# This does the sbox mapping
def sbox_trans(h_list):
    to_return = []
    for i in h_list:
        # If hex is bigger than 15 then we don't have a leading zero
        if int(i, 16) > 15:
            a = int(i[2], 16)
            b = int(i[3], 16)
        # Else it has a leading zero
        else:
            a = 0
            b = int(i[2], 16)
        to_return.append(hex(S_BOX[a][b]))

    return to_return

# This does the inverse sbox mapping
def isbox_trans(h_list):
    to_return = []
    for i in h_list:
        # If hex is bigger than 15 then we don't have a leading zero
        if int(i, 16) > 15:
            a = int(i[2], 16)
            b = int(i[3], 16)
        # Else it has a leading zero
        else:
            a = 0
            b = int(i[2], 16)
        to_return.append(hex(InS_BOX[a][b]))

    return to_return

# uses the shift rows algorithm
def shift_rows(h_list):
    to_return = []
    for i in range(0, 4):
            to_return.append(h_list[i])

    to_return.append(h_list[5])
    to_return.append(h_list[6])
    to_return.append(h_list[7])
    to_return.append(h_list[4])

    to_return.append(h_list[10])
    to_return.append(h_list[11])
    to_return.append(h_list[8])
    to_return.append(h_list[9])

    to_return.append(h_list[15])
    to_return.append(h_list[12])
    to_return.append(h_list[13])
    to_return.append(h_list[14])

    return to_return

# Inverse shift row
def ishift_rows(h_list):
    to_return = []
    for i in range(0, 4):
        to_return.append(h_list[i])

    to_return.append(h_list[7])
    to_return.append(h_list[4])
    to_return.append(h_list[5])
    to_return.append(h_list[6])

    to_return.append(h_list[10])
    to_return.append(h_list[11])
    to_return.append(h_list[8])
    to_return.append(h_list[9])

    to_return.append(h_list[13])
    to_return.append(h_list[14])
    to_return.append(h_list[15])
    to_return.append(h_list[12])

    return to_return


def mix_cols(h_list):
    to_return = []

    new_list = rearrange(h_list)

    for i in mix:
        start, end = 0, 4
        col = i
        for r in range(0, 4):
            row = []
            sum = []

            for k in range(start, end):
                row.append(new_list[k])

            for j in range(0, 4):
                # Here we calculate 3 * hex_number.
                # The ifs statements are to deal with some shenanigans with bin() function
                if col[j] == 3:
                    bit1 = bin(int(row[j], 16))
                    if len(bit1[2:]) < 8:
                        if int(bit1[0]) == 1:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = (int(bit1[3:], 2) ^ 27) ^ int(row[j], 16)
                            else:
                                bit1 = (int(bit1, 2) ^ 27) ^ int(row[j], 16)
                            sum.append(bit1)
                        else:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = int(bit1[3:], 2) ^ int(row[j], 16)
                            else:
                                bit1 = int(bit1, 2) ^ int(row[j], 16)
                            sum.append(bit1)
                    else:
                        if int(bit1[2]) == 1:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = (int(bit1[3:], 2) ^ 27) ^ int(row[j], 16)
                            else:
                                bit1 = (int(bit1, 2) ^ 27) ^ int(row[j], 16)
                            sum.append(bit1)
                        else:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = int(bit1[3:], 2) ^ int(row[j], 16)
                            else:
                                bit1 = int(bit1, 2) ^ int(row[j], 16)
                            sum.append(bit1)
                # Here we calculate 2 * hex_number. Similar to function mult_by_2.
                elif col[j] == 2:
                    bit1 = bin(int(row[j], 16))
                    if len(bit1[2:]) < 8:
                        if int(bit1[0]) == 1:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = (int(bit1[3:], 2) ^ 27)
                            else:
                                bit1 = (int(bit1, 2) ^ 27)
                            sum.append(bit1)
                        else:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = (int(bit1[3:], 2))
                            else:
                                bit1 = (int(bit1, 2))
                            sum.append(bit1)
                    else:
                        if int(bit1[2]) == 1:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = (int(bit1[3:], 2) ^ 27)
                            else:
                                bit1 = (int(bit1, 2) ^ 27)
                            sum.append(bit1)
                        else:
                            bit1 = bin(int(row[j], 16) << 1)
                            if len(bit1[2:]) > 8:
                                bit1 = (int(bit1[3:], 2))
                            else:
                                bit1 = (int(bit1, 2))
                            sum.append(bit1)
                # Here we calculate 1 * hex_number. Essentially just send the number back.
                else:
                    sum.append(int(row[j], 16))

            to_return.append(hex(sum[0] ^ sum[1] ^ sum[2] ^ sum[3]))
            start += 4
            end += 4

    return to_return

def imix_cols(h_list):
    to_return = []
    new_list = rearrange(h_list)
    for i in inv_mix:
        start, end = 0, 4
        col = i
        for r in range(0, 4):
            row = []
            sum = []

            for k in range(start, end):
                row.append(new_list[k])
            # Here we calculate 9 * hex_number = (((hex_number * 2) * 2) * 2) + hex_number
            for j in range(0, 4):
                if col[j] == 9:
                    bit1 = mult_by_2(row[j])
                    bit1 = mult_by_2(bit1)
                    bit1 = mult_by_2(bit1)
                    bit1 = int(bit1, 16) ^ int(row[j], 16)
                    sum.append(bit1)
                # Here we calculate 11 * hex_number = ((((hex_number * 2) * 2) + hex_number) * 2) +  hex_number
                elif col[j] == 11:
                    bit1 = mult_by_2(row[j])
                    bit1 = hex(int(mult_by_2(bit1), 16) ^ int(row[j], 16))
                    bit1 = int(mult_by_2(bit1), 16) ^ int(row[j], 16)
                    sum.append(bit1)
                # Here we calculate 13 *  hex_number = ((((hex_number * 2) +  hex_number) * 2) * 2) +  hex_number
                elif col[j] == 13:
                    bit1 = hex(int(mult_by_2(row[j]), 16) ^ int(row[j], 16))
                    bit1 = mult_by_2(bit1)
                    bit1 = mult_by_2(bit1)
                    bit1 = int(bit1, 16) ^ int(row[j], 16)
                    sum.append(bit1)
                # Here we calculate 14 *  hex_number = ((((hex_number * 2) +  hex_number) * 2) + hex_number) * 2
                else:
                    bit1 = hex(int(mult_by_2(row[j]), 16) ^ int(row[j], 16))
                    bit1 = hex(int(mult_by_2(bit1), 16) ^ int(row[j], 16))
                    bit1 = int(mult_by_2(bit1), 16)
                    sum.append(bit1)

            to_return.append(hex(sum[0] ^ sum[1] ^ sum[2] ^ sum[3]))
            start += 4
            end += 4
    return to_return

# This is essentially calculating the transpose of a matrix
def rearrange(h_list):
    to_return = []
    to_return.append(h_list[0])
    to_return.append(h_list[4])
    to_return.append(h_list[8])
    to_return.append(h_list[12])

    to_return.append(h_list[1])
    to_return.append(h_list[5])
    to_return.append(h_list[9])
    to_return.append(h_list[13])

    to_return.append(h_list[2])
    to_return.append(h_list[6])
    to_return.append(h_list[10])
    to_return.append(h_list[14])

    to_return.append(h_list[3])
    to_return.append(h_list[7])
    to_return.append(h_list[11])
    to_return.append(h_list[15])

    return to_return

# Helper function for key_schedule
def rotWord(k_list):
    to_return = []
    to_return.append(k_list[1])
    to_return.append(k_list[2])
    to_return.append(k_list[3])
    to_return.append(k_list[0])

    return to_return


# This follows the key-schedule step algorithm
def key_schedule(k_list1, round):

    k_list2 = rearrange(k_list1)
    key_to_return = []
    k_words = []
    start, end, count = 0, 4, 0

    while count < 4:
        a = k_list2[start:end]
        k_words.append(a)
        count += 1
        start += 4
        end += 4
    w3 = rotWord(k_words[3])
    w3 = sbox_trans(w3)
    w3 = [hex(int(w3[0], 16) ^ round_const[round]), w3[1], w3[2], w3[3]]

    w4 = []
    for i in range(0, 4):
        w4.append(hex(int(k_words[0][i], 16) ^ int(w3[i], 16)))

    for i in w4:
        key_to_return.append(i)

    w5 = []
    for i in range(0, 4):
        w5.append(hex(int(k_words[1][i], 16) ^ int(w4[i], 16)))

    for i in w5:
        key_to_return.append(i)

    w6 = []
    for i in range(0, 4):
        w6.append(hex(int(k_words[2][i], 16) ^ int(w5[i], 16)))

    for i in w6:
        key_to_return.append(i)

    w7 = []
    for i in range(0, 4):
        w7.append(hex(int(k_words[3][i], 16) ^ int(w6[i], 16)))

    for i in w7:
        key_to_return.append(i)

    return rearrange(key_to_return)


# Secret key
key = "I'm so tired man"

l_key = []

# Convert key to hex values
for i in key:
    l_key.append(hex(ord(i)))

l_key = rearrange(l_key)

s = input("Please enter a string: ")
plain_text = []
a = []

count = 0

# Create the 128-bit blocks
for i in s:
    a.append(hex(int(hex(ord(i)), 16)))
    count += 1
    if count > 15:
        b = a[:]
        plain_text.append(b)
        a.clear()
        count = 0

if count != 0:
    plain_text.append(a)

for i in plain_text:
    if len(i) != 16:
        while len(i) < 16:
            i.append(hex(0))

for i in range(0, len(plain_text)):
    plain_text[i] = rearrange(plain_text[i])


# Start of the encryption
cypher_text = []

round_keys = []

key_schedule(l_key, 0)

round_keys.append(l_key)

# Create keys
round = 0
while round < 10:
    round_keys.append(key_schedule(round_keys[round], round))
    round += 1

# Encryption
for i in plain_text:
    round = 0
    new = ark(i, round_keys[round])
    while round < 10:
        trans = sbox_trans(new)
        shifted = shift_rows(trans)
        if round != 9:
            mixed = mix_cols(shifted)
        round += 1
        if round == 10:
            new = ark(shifted, round_keys[round])
        else:
            new = ark(mixed, round_keys[round])

    cypher_text.append(new)


print("The Encrypted string is: ")
encryt_key = ""

for i in cypher_text:
    for j in rearrange(i):
        encryt_key = encryt_key + chr(int(j, 16))

print(encryt_key)

# Start of the decryption
plain_text = []
round_keys.reverse()


for i in cypher_text:
    round = 0
    new = ark(i, round_keys[round])
    while round < 10:
        trans = isbox_trans(new)
        shifted = ishift_rows(trans)
        if round != 9:
            mixed = imix_cols(shifted)
        round += 1
        if round == 10:
            new = ark(shifted, round_keys[round])
        else:
            new = ark(mixed, imix_cols(round_keys[round]))
    plain_text.append(new)

decrypt_key = ""
for i in plain_text:
    for j in rearrange(i):
        if int(j, 16) != 0:
            decrypt_key = decrypt_key + chr(int(j, 16))

print("The Encrypted string is: ")
print(decrypt_key)







