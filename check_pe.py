import struct
import os

dll_path = r"C:\Program Files (x86)\Compac\COMERCIAL\MGW_SDK.dll"
print("Checking PE Import Table for:", dll_path)

with open(dll_path, "rb") as f:
    data = f.read()

pe_off = struct.unpack_from("<I", data, 0x3c)[0]
num_sections = struct.unpack_from("<H", data, pe_off + 6)[0]
opt_hdr_size = struct.unpack_from("<H", data, pe_off + 20)[0]
import_rva = struct.unpack_from("<I", data, pe_off + 0x80)[0]

sections = []
sec_off = pe_off + 24 + opt_hdr_size
for i in range(num_sections):
    name = data[sec_off:sec_off+8].decode("latin1").rstrip("\x00")
    vsize, va, rsize, raw = struct.unpack_from("<IIII", data, sec_off + 8)
    sections.append((va, vsize, raw, rsize))
    sec_off += 40

def rva_to_offset(rva):
    for va, vsize, raw, rsize in sections:
        if va <= rva < va + vsize:
            return raw + (rva - va)
    return None

import_off = rva_to_offset(import_rva)
print(f"Import Table Offset: {import_off}")
if import_off:
    idx = import_off
    while True:
        original_first_thunk, timestamp, forwarder, name_rva, first_thunk = struct.unpack_from("<IIIII", data, idx)
        if name_rva == 0: break
        name_off = rva_to_offset(name_rva)
        if name_off:
            end = data.find(b"\x00", name_off)
            dll_name = data[name_off:end].decode("latin1")
            print("Imported DLL:", dll_name)
        idx += 20
