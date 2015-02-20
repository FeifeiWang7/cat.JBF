//
// EnvelopedCmsTest.cs - NUnit tests for EnvelopedCms
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// (C) 2003 Motus Technologies Inc. (http://www.motus.com)
// Copyright (C) 2004-2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if NET_2_0

using NUnit.Framework;

using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace MonoTests.System.Security.Cryptography.Pkcs {

	[TestFixture]
	public class EnvelopedCmsTest : Assertion {

		static byte[] asnNull = { 0x05, 0x00 };
		static string tdesOid = "1.2.840.113549.3.7";
		static string tdesName = "3des";
		static string p7DataOid = "1.2.840.113549.1.7.1";
		static string p7DataName = "PKCS 7 Data";

		static public byte [] farscape_p12_pfx = { 
			0x30, 0x82, 0x07, 0x17, 0x02, 0x01, 0x03, 0x30, 0x82, 0x06, 0xD3, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0xA0, 0x82, 0x06, 0xC4, 0x04, 0x82, 0x06, 0xC0, 0x30, 0x82, 0x06, 0xBC, 0x30, 0x82, 0x03, 0xCD, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0xA0, 0x82, 0x03, 0xBE, 0x04, 0x82, 0x03, 0xBA, 0x30, 0x82, 0x03, 0xB6, 0x30, 0x82, 0x03, 0xB2, 0x06, 0x0B, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x0C, 0x0A, 0x01, 0x02, 0xA0, 0x82, 0x02, 0xB6, 0x30, 0x82, 0x02, 0xB2, 0x30, 0x1C, 0x06, 0x0A, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x0C, 0x01, 0x03, 0x30, 
			0x0E, 0x04, 0x08, 0x86, 0x2A, 0xA9, 0x71, 0x6D, 0xA4, 0xB8, 0x2D, 0x02, 0x02, 0x07, 0xD0, 0x04, 0x82, 0x02, 0x90, 0x90, 0x14, 0xB5, 0xF0, 0xB6, 0x86, 0x56, 0xCB, 0xFA, 0x63, 0xAD, 0x9F, 0x5A, 0x59, 0x6C, 0xAD, 0x00, 0x3C, 0x37, 0x8A, 0xC3, 0x88, 0x58, 0x8B, 0xD7, 0x48, 0x53, 0x7A, 0xC8, 0x5B, 0x0D, 0x98, 0xDD, 0x8B, 0xB3, 0xEC, 0x4C, 0xAC, 0x61, 0x18, 0xE3, 0x5E, 0x47, 0xAD, 0xC7, 0x92, 0xBC, 0xD3, 0x00, 0x07, 0xFF, 0x1A, 0x68, 0x74, 0x45, 0x8E, 0xD8, 0x7C, 0x9F, 0x18, 0x7B, 0xD7, 0xC8, 0x47, 0xBA, 0x6B, 0x19, 0xF2, 0xBF, 0x7E, 0x51, 0x0B, 0x4B, 0x43, 0xE8, 0xB9, 0x56, 0x7E, 0xD0, 0x74, 0xC7, 
			0xDE, 0x76, 0xDB, 0xFF, 0x5C, 0x6B, 0x53, 0xBE, 0x31, 0x06, 0xAE, 0x6C, 0x8F, 0xDC, 0x49, 0x04, 0x71, 0x74, 0xEE, 0xB8, 0x06, 0xCB, 0xAD, 0x86, 0xB4, 0x4E, 0xB9, 0x46, 0xA1, 0x03, 0x5E, 0x0E, 0xA7, 0xC7, 0x37, 0x6B, 0xB0, 0x8D, 0x2D, 0x81, 0x1F, 0xE3, 0xC2, 0x05, 0xDE, 0xEF, 0x51, 0x07, 0x70, 0x6E, 0x35, 0x9A, 0xAD, 0x19, 0x5E, 0xAF, 0xEB, 0x7F, 0xEF, 0xE4, 0xAB, 0x07, 0xF3, 0xF6, 0xEA, 0xFA, 0x0E, 0x83, 0x65, 0x06, 0x3C, 0xF3, 0xBD, 0x96, 0x08, 0x14, 0xC5, 0x34, 0x26, 0xED, 0xC0, 0x10, 0xCC, 0xAE, 0x2D, 0x8F, 0xBE, 0xED, 0x98, 0x0D, 0x88, 0x1B, 0x1E, 0xC7, 0x37, 0xF0, 0xFC, 0xDB, 0x3C, 0xE3, 
			0x1B, 0x66, 0x52, 0x45, 0x6E, 0x05, 0xA6, 0xD9, 0x12, 0x23, 0x05, 0x5F, 0xE3, 0x9F, 0x7D, 0x21, 0x9B, 0x2E, 0x3E, 0x9E, 0x3C, 0xEE, 0xD1, 0x9B, 0x55, 0xDE, 0x57, 0x60, 0xA5, 0x24, 0x2D, 0xC7, 0x94, 0xEC, 0xFC, 0xB1, 0x6A, 0x65, 0xBD, 0x85, 0x02, 0x5C, 0x58, 0xAA, 0x5A, 0x6A, 0xF3, 0xAC, 0x6B, 0xDD, 0x0E, 0x63, 0xB2, 0x4B, 0x5B, 0x67, 0x3D, 0xC3, 0xBF, 0xE4, 0xC8, 0xEF, 0x3F, 0x89, 0x5A, 0xCD, 0x6D, 0xEF, 0x05, 0x22, 0x2B, 0x72, 0xFF, 0x80, 0x7A, 0xDD, 0xF1, 0x59, 0xA7, 0x6F, 0x00, 0xB1, 0xBD, 0x4D, 0x88, 0xD6, 0xE4, 0x8A, 0xDD, 0xA9, 0xFC, 0xD9, 0x01, 0x0A, 0x65, 0x8E, 0x52, 0xF9, 0x7E, 0x20, 
			0x72, 0x67, 0x0D, 0x5B, 0xEE, 0x67, 0x5B, 0x46, 0x4A, 0x15, 0xA2, 0x6F, 0x15, 0x2B, 0x5B, 0x9A, 0x93, 0x12, 0x4F, 0xF4, 0xAD, 0x49, 0xD0, 0x11, 0xF1, 0x7E, 0x40, 0xDE, 0x32, 0x96, 0x2E, 0xB3, 0xE8, 0x71, 0x60, 0x27, 0x6E, 0xA2, 0x71, 0x83, 0xC7, 0xFE, 0x0E, 0x8B, 0x31, 0x06, 0x64, 0xE1, 0x19, 0x02, 0xB9, 0x44, 0x25, 0x0C, 0x94, 0x64, 0x7E, 0x5F, 0x89, 0x4D, 0x7E, 0x99, 0x0B, 0x91, 0xB8, 0x22, 0xA5, 0x33, 0x92, 0xD3, 0x49, 0x07, 0x1D, 0xC6, 0x25, 0x4A, 0xD7, 0x6D, 0xE2, 0x94, 0x3F, 0xFA, 0x10, 0x72, 0x59, 0x62, 0xF5, 0xC6, 0xD4, 0x3A, 0xEE, 0x8F, 0xBC, 0x9C, 0xBC, 0xFC, 0xC7, 0x37, 0xBF, 0x7C, 
			0xA0, 0x67, 0xB0, 0xFF, 0x0F, 0x29, 0xA0, 0xA2, 0x71, 0x6B, 0x21, 0x00, 0xF4, 0x54, 0xD9, 0x3D, 0x1B, 0xCE, 0xF4, 0xFE, 0x6F, 0xF5, 0x21, 0xCB, 0x47, 0x58, 0x17, 0xF6, 0x45, 0x2F, 0xA0, 0x3B, 0x8B, 0xD9, 0xB8, 0x8A, 0x33, 0x3F, 0x16, 0xE0, 0xC7, 0x8A, 0xB8, 0x11, 0x2F, 0xA8, 0x7E, 0x7D, 0xA7, 0x7B, 0x65, 0x27, 0x89, 0x3C, 0x67, 0x4D, 0xD5, 0x70, 0x28, 0x76, 0x60, 0x96, 0x68, 0xBF, 0xFB, 0xCD, 0x49, 0xE0, 0x8A, 0x7C, 0x6F, 0x76, 0x06, 0x48, 0x6D, 0x63, 0x67, 0x8A, 0x47, 0x82, 0x5E, 0x7F, 0x0E, 0xAC, 0x46, 0xB6, 0xBC, 0x0A, 0x6D, 0xE2, 0x1A, 0x3A, 0x20, 0xA5, 0xC7, 0x81, 0x71, 0x6E, 0x2B, 0x16, 
			0x97, 0xD4, 0xFA, 0xC0, 0xDD, 0x72, 0x5B, 0x9F, 0xA3, 0x43, 0xF4, 0x85, 0xB1, 0xC6, 0xA8, 0xE0, 0x62, 0x81, 0x5D, 0xA5, 0x07, 0x29, 0x6A, 0x6A, 0x2D, 0xE1, 0x1D, 0xBE, 0x12, 0x6D, 0x42, 0x58, 0x6F, 0x4E, 0x30, 0x3D, 0xBF, 0x32, 0x11, 0x38, 0xBC, 0x36, 0x76, 0x60, 0xFC, 0x57, 0x2F, 0xD3, 0x9E, 0xC4, 0x1A, 0x92, 0xEA, 0xDE, 0x85, 0xFD, 0xE7, 0xAA, 0x30, 0xA6, 0x97, 0x2C, 0x36, 0x3B, 0x3B, 0x0E, 0x92, 0x52, 0xFF, 0x42, 0xD7, 0x62, 0x6C, 0xC1, 0x3A, 0xE7, 0x1B, 0x4E, 0x13, 0x8C, 0x95, 0xB3, 0x4B, 0xA7, 0x9E, 0x42, 0x75, 0xA8, 0xCA, 0x63, 0x76, 0xC4, 0x45, 0x74, 0x96, 0x43, 0xD8, 0x86, 0x82, 0xBE, 
			0x37, 0xFF, 0x9B, 0xEB, 0xB7, 0x18, 0xA1, 0x2F, 0xE3, 0x6C, 0x08, 0xE8, 0x11, 0x96, 0x8C, 0x5E, 0x9E, 0x2B, 0xE7, 0xDB, 0x7D, 0x54, 0xE1, 0xDB, 0x1E, 0xD3, 0x8F, 0xB5, 0x19, 0x4B, 0xB2, 0x16, 0xDB, 0xCF, 0xEC, 0x88, 0x0B, 0x6C, 0x3C, 0xE4, 0xF2, 0xC4, 0xFF, 0x4D, 0x3E, 0x53, 0x52, 0x3A, 0x81, 0x0B, 0x6E, 0xAC, 0x95, 0xEA, 0x5A, 0x6E, 0x4D, 0x83, 0x23, 0x82, 0xC9, 0x90, 0x02, 0x74, 0x10, 0x2A, 0x6C, 0xFB, 0x97, 0x4F, 0x5F, 0x70, 0x8E, 0xF0, 0xB9, 0x31, 0x81, 0xE8, 0x30, 0x0D, 0x06, 0x09, 0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x11, 0x02, 0x31, 0x00, 0x30, 0x13, 0x06, 0x09, 0x2A, 0x86, 0x48, 
			0x86, 0xF7, 0x0D, 0x01, 0x09, 0x15, 0x31, 0x06, 0x04, 0x04, 0x01, 0x00, 0x00, 0x00, 0x30, 0x57, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x09, 0x14, 0x31, 0x4A, 0x1E, 0x48, 0x00, 0x64, 0x00, 0x64, 0x00, 0x62, 0x00, 0x30, 0x00, 0x65, 0x00, 0x64, 0x00, 0x31, 0x00, 0x64, 0x00, 0x2D, 0x00, 0x32, 0x00, 0x36, 0x00, 0x30, 0x00, 0x34, 0x00, 0x2D, 0x00, 0x34, 0x00, 0x32, 0x00, 0x35, 0x00, 0x66, 0x00, 0x2D, 0x00, 0x38, 0x00, 0x31, 0x00, 0x35, 0x00, 0x66, 0x00, 0x2D, 0x00, 0x34, 0x00, 0x39, 0x00, 0x35, 0x00, 0x61, 0x00, 0x37, 0x00, 0x64, 0x00, 0x65, 0x00, 0x65, 0x00, 0x37, 0x00, 0x61, 0x00, 
			0x64, 0x00, 0x30, 0x30, 0x69, 0x06, 0x09, 0x2B, 0x06, 0x01, 0x04, 0x01, 0x82, 0x37, 0x11, 0x01, 0x31, 0x5C, 0x1E, 0x5A, 0x00, 0x4D, 0x00, 0x69, 0x00, 0x63, 0x00, 0x72, 0x00, 0x6F, 0x00, 0x73, 0x00, 0x6F, 0x00, 0x66, 0x00, 0x74, 0x00, 0x20, 0x00, 0x52, 0x00, 0x53, 0x00, 0x41, 0x00, 0x20, 0x00, 0x53, 0x00, 0x43, 0x00, 0x68, 0x00, 0x61, 0x00, 0x6E, 0x00, 0x6E, 0x00, 0x65, 0x00, 0x6C, 0x00, 0x20, 0x00, 0x43, 0x00, 0x72, 0x00, 0x79, 0x00, 0x70, 0x00, 0x74, 0x00, 0x6F, 0x00, 0x67, 0x00, 0x72, 0x00, 0x61, 0x00, 0x70, 0x00, 0x68, 0x00, 0x69, 0x00, 0x63, 0x00, 0x20, 0x00, 0x50, 0x00, 0x72, 0x00, 0x6F, 
			0x00, 0x76, 0x00, 0x69, 0x00, 0x64, 0x00, 0x65, 0x00, 0x72, 0x30, 0x82, 0x02, 0xE7, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x06, 0xA0, 0x82, 0x02, 0xD8, 0x30, 0x82, 0x02, 0xD4, 0x02, 0x01, 0x00, 0x30, 0x82, 0x02, 0xCD, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0x30, 0x1C, 0x06, 0x0A, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x0C, 0x01, 0x06, 0x30, 0x0E, 0x04, 0x08, 0x0B, 0x02, 0xBA, 0x83, 0x5D, 0x71, 0x1D, 0xBD, 0x02, 0x02, 0x07, 0xD0, 0x80, 0x82, 0x02, 0xA0, 0x52, 0xD1, 0x51, 0x2A, 0xD1, 0x6D, 0x7E, 0xB0, 0x0A, 0x62, 0x6C, 0x0B, 0xE6, 0x6C, 0x72, 
			0x65, 0x3F, 0x89, 0x36, 0x1F, 0x71, 0x85, 0x00, 0x45, 0xC9, 0x56, 0x65, 0xC6, 0x43, 0xF6, 0xC1, 0x56, 0x81, 0xF0, 0xF5, 0x73, 0x57, 0xA5, 0x47, 0x45, 0xB6, 0xBD, 0xC3, 0xEB, 0xE0, 0xE0, 0x0F, 0x23, 0xCE, 0x95, 0xD7, 0x85, 0xCA, 0x73, 0xC0, 0x0E, 0x36, 0x7A, 0xF1, 0x01, 0x3F, 0x0B, 0x1C, 0xC2, 0x6C, 0x09, 0xC2, 0x43, 0x98, 0x14, 0x10, 0x80, 0x35, 0xF0, 0x45, 0x7A, 0x4F, 0x1F, 0x90, 0x3F, 0xD8, 0x08, 0xC6, 0x09, 0x22, 0xA3, 0xFD, 0x7A, 0x54, 0xB4, 0x27, 0x53, 0x20, 0x42, 0xE8, 0x89, 0xBE, 0xDC, 0x13, 0xCE, 0x9B, 0x76, 0x8F, 0xBB, 0x19, 0xA3, 0x54, 0x6E, 0xCB, 0x3C, 0x09, 0x7C, 0xC1, 0xD4, 0xCE, 
			0xF0, 0xFF, 0x95, 0xA0, 0xB6, 0x41, 0x07, 0xC0, 0xDD, 0x57, 0x36, 0xDC, 0x45, 0x65, 0xA2, 0xC8, 0xB3, 0x72, 0x3F, 0x99, 0xCA, 0x2C, 0xA0, 0x24, 0x06, 0x1E, 0xF9, 0xD3, 0xBB, 0xF4, 0x70, 0xA5, 0x2B, 0xCB, 0xFE, 0x14, 0x73, 0x8E, 0x83, 0x7A, 0x50, 0xA0, 0xB3, 0x80, 0xBC, 0xDA, 0xEF, 0x1D, 0x68, 0x35, 0xE9, 0x62, 0x3D, 0xA6, 0x0D, 0x0A, 0xF3, 0x06, 0x2C, 0x87, 0x7B, 0xC6, 0x83, 0x27, 0x1F, 0x22, 0x1E, 0xC3, 0x37, 0xD1, 0xB3, 0x81, 0x2B, 0x0E, 0xCA, 0x36, 0x2B, 0x45, 0x2C, 0xAE, 0x09, 0x23, 0xA4, 0xFF, 0xB0, 0xE6, 0x13, 0x70, 0x89, 0xB7, 0x2A, 0xD7, 0x94, 0x35, 0x1F, 0x73, 0x0E, 0x50, 0xF3, 0x5E, 
			0x92, 0xC3, 0xE7, 0x8E, 0x29, 0x32, 0xED, 0x3F, 0xCC, 0x34, 0x53, 0x54, 0xE5, 0xA1, 0x50, 0x93, 0x40, 0x95, 0x47, 0x29, 0x4B, 0x59, 0x4D, 0x28, 0xBC, 0x2F, 0xA9, 0x5F, 0xF8, 0x27, 0x22, 0x49, 0xDB, 0x66, 0xA6, 0x24, 0xE0, 0xF2, 0xF0, 0x0F, 0xCC, 0x7B, 0xE4, 0x55, 0x0D, 0xB4, 0x20, 0x73, 0xB9, 0x29, 0xA4, 0x7F, 0xDD, 0x46, 0xA0, 0x47, 0x3A, 0x03, 0x20, 0xBD, 0x6E, 0xF6, 0x88, 0x18, 0x02, 0xD2, 0xD9, 0x4F, 0xC6, 0x55, 0xA7, 0x82, 0xDB, 0x32, 0x5B, 0x1A, 0x74, 0x8D, 0xBD, 0xD8, 0x66, 0x3D, 0x0E, 0x43, 0xFE, 0x6A, 0x5E, 0xD8, 0x23, 0x04, 0x6A, 0x0F, 0x75, 0xC1, 0xCA, 0xD1, 0x04, 0xDB, 0x8D, 0x7F, 
			0x21, 0xCA, 0xE6, 0xF0, 0x3D, 0x15, 0x23, 0x87, 0x52, 0xBE, 0x8E, 0xAA, 0x4B, 0xA2, 0xFA, 0xAE, 0x33, 0xD3, 0xB4, 0x9A, 0x54, 0xCC, 0xA3, 0xE1, 0xB1, 0x6C, 0xD7, 0xA4, 0x51, 0x7B, 0x8F, 0x58, 0x01, 0x8C, 0xC3, 0xE9, 0x49, 0xB8, 0xB8, 0x01, 0x3B, 0x0D, 0x94, 0x16, 0xF4, 0x47, 0xA4, 0x9C, 0x20, 0x97, 0x35, 0x2A, 0x10, 0xCA, 0xA8, 0xB5, 0xDA, 0x0F, 0x2D, 0x0C, 0x7D, 0xA0, 0x55, 0x17, 0x9C, 0x55, 0xEA, 0x6F, 0x7D, 0xE3, 0x3B, 0xB3, 0x81, 0x0F, 0x4E, 0xD0, 0x0B, 0x88, 0x1A, 0xF6, 0xB4, 0x0F, 0x15, 0x18, 0xC5, 0x54, 0x4C, 0xF1, 0x15, 0x88, 0xAD, 0x03, 0x7E, 0x0E, 0x88, 0x34, 0xB6, 0xCF, 0x96, 0x9B, 
			0x70, 0xC9, 0x16, 0x8D, 0x63, 0xB2, 0xF6, 0x4C, 0x05, 0x7D, 0x45, 0x5F, 0xD7, 0xA7, 0xE0, 0xBC, 0xA0, 0xBE, 0xBF, 0x8B, 0x70, 0x08, 0x90, 0x93, 0x32, 0xE0, 0x23, 0x84, 0x26, 0x76, 0x85, 0x03, 0x19, 0xF7, 0xE3, 0x66, 0x41, 0xAD, 0x60, 0xEE, 0xED, 0x4D, 0x7F, 0xC7, 0xB7, 0xE4, 0xE2, 0x0B, 0xCC, 0x5C, 0x12, 0x18, 0xD8, 0xF8, 0x2E, 0x24, 0x7A, 0x4D, 0x66, 0x10, 0x9C, 0xAC, 0xF6, 0xD8, 0x51, 0x69, 0x77, 0x58, 0xD0, 0xF5, 0x15, 0xB7, 0xF0, 0xA0, 0x2F, 0xB9, 0x13, 0x8B, 0x65, 0x77, 0x1A, 0x02, 0xB1, 0xD1, 0x86, 0x25, 0xFB, 0xD5, 0x44, 0x9D, 0xBB, 0x2D, 0xF9, 0x7D, 0x77, 0xB8, 0x7F, 0x5A, 0x34, 0x08, 
			0x0B, 0x8C, 0xBE, 0x6C, 0xBD, 0xF4, 0xD0, 0x9A, 0x1E, 0x77, 0x94, 0xB3, 0x37, 0x5F, 0xED, 0x4C, 0x0D, 0x18, 0x58, 0xD1, 0x5F, 0x7D, 0xD7, 0x1A, 0xBD, 0x6D, 0x3A, 0xEF, 0xAA, 0x7B, 0xAF, 0x60, 0xB9, 0x6A, 0x89, 0x36, 0x27, 0xF1, 0xCA, 0x0F, 0xD4, 0x8D, 0x75, 0xA7, 0x62, 0x0C, 0x95, 0x4E, 0xA1, 0x03, 0xEE, 0x06, 0x5C, 0x6C, 0x3F, 0x6F, 0x37, 0x3E, 0xCE, 0x9B, 0x26, 0x89, 0x4E, 0xDD, 0x9E, 0x57, 0x72, 0xB7, 0xD7, 0xE6, 0x25, 0xB8, 0xDA, 0x91, 0x11, 0xB2, 0xB6, 0x89, 0x18, 0x42, 0xDF, 0xA6, 0x1E, 0xB5, 0x13, 0x1D, 0x90, 0x21, 0x48, 0x75, 0x58, 0x0C, 0x0A, 0x22, 0xC2, 0x07, 0x12, 0x9B, 0x73, 0x6E, 
			0x0F, 0xCE, 0x10, 0x28, 0x3D, 0x2A, 0x45, 0x64, 0x60, 0xE3, 0xB7, 0xE1, 0x76, 0x90, 0xEC, 0x5B, 0xC6, 0xA1, 0xF0, 0xC4, 0xE8, 0x12, 0xD9, 0xC6, 0x22, 0x80, 0xB5, 0x30, 0xE5, 0x17, 0xAE, 0x05, 0x96, 0xBB, 0x4E, 0xBB, 0x33, 0xBB, 0xB0, 0x63, 0x29, 0x74, 0x11, 0x06, 0x23, 0x36, 0xB4, 0xA1, 0x25, 0xD5, 0x2A, 0xF3, 0x90, 0x38, 0x18, 0x02, 0x62, 0x30, 0x3B, 0x30, 0x1F, 0x30, 0x07, 0x06, 0x05, 0x2B, 0x0E, 0x03, 0x02, 0x1A, 0x04, 0x14, 0xDC, 0x3A, 0xAB, 0x36, 0xD7, 0x3E, 0xF4, 0x6C, 0x52, 0xC9, 0x89, 0x37, 0xFE, 0x71, 0x71, 0x83, 0xC6, 0x09, 0x88, 0xDD, 0x04, 0x14, 0xF5, 0x76, 0xC2, 0xCC, 0xB9, 0xE5, 
			0xF5, 0x28, 0xA3, 0x2D, 0x55, 0xDC, 0xDE, 0x3B, 0xCF, 0x53, 0xEE, 0x4B, 0x8F, 0x6F, 0x02, 0x02, 0x07, 0xD0 };

		private void DefaultProperties (EnvelopedCms ep, int contentLength, int version) 
		{
			AssertEquals ("Certificates", 0, ep.Certificates.Count);
			AssertEquals ("ContentEncryptionAlgorithm.KeyLength", 0, ep.ContentEncryptionAlgorithm.KeyLength);
			AssertEquals ("ContentEncryptionAlgorithm.Oid.FriendlyName", tdesName, ep.ContentEncryptionAlgorithm.Oid.FriendlyName);
			AssertEquals ("ContentEncryptionAlgorithm.Oid.Value", tdesOid, ep.ContentEncryptionAlgorithm.Oid.Value);
			AssertEquals ("ContentEncryptionAlgorithm.Parameters", 0, ep.ContentEncryptionAlgorithm.Parameters.Length);
			AssertEquals ("ContentInfo.ContentType.FriendlyName", p7DataName, ep.ContentInfo.ContentType.FriendlyName);
			AssertEquals ("ContentInfo.ContentType.Value", p7DataOid, ep.ContentInfo.ContentType.Value);
			AssertEquals ("ContentInfo.Content", contentLength, ep.ContentInfo.Content.Length);
			AssertEquals ("RecipientInfos", 0, ep.RecipientInfos.Count);
			AssertEquals ("UnprotectedAttributes", 0, ep.UnprotectedAttributes.Count);
			AssertEquals ("Version", version, ep.Version);
		}

		private X509Certificate2 GetCertificate (bool includePrivateKey) 
		{
			return new X509Certificate2 (farscape_p12_pfx, "farscape");
		}

		[Test]
		public void ConstructorEmpty () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			DefaultProperties (ep, 0, 0);
		}

		[Test]
		public void ConstructorContentInfo () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (ci);
			DefaultProperties (ep, asnNull.Length, 0);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void ConstructorContentInfoNull () 
		{
			EnvelopedCms ep = new EnvelopedCms (null);
		}

		[Test]
		public void ConstructorContentInfoAlgorithmIdentifier () 
		{
			AlgorithmIdentifier ai = new AlgorithmIdentifier ();
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (ci, ai);
			DefaultProperties (ep, 2, 0);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void ConstructorContentInfoNullAlgorithmIdentifier () 
		{
			AlgorithmIdentifier ai = new AlgorithmIdentifier ();
			EnvelopedCms ep = new EnvelopedCms (null, ai);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void ConstructorContentInfoAlgorithmIdentifierNull () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (ci, null);
		}

		[Test]
		public void ConstructorSubjectIdentifierTypeIssuerAndSerialNumberContentInfoAlgorithmIdentifier () 
		{
			AlgorithmIdentifier ai = new AlgorithmIdentifier ();
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.IssuerAndSerialNumber, ci, ai);
			DefaultProperties (ep, 2, 0);
		}

		[Test]
		public void ConstructorSubjectIdentifierTypeSubjectKeyIdentifierContentInfoAlgorithmIdentifier () 
		{
			AlgorithmIdentifier ai = new AlgorithmIdentifier ();
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.SubjectKeyIdentifier, ci, ai);
			DefaultProperties (ep, 2, 2);
		}

		[Test]
		public void ConstructorSubjectIdentifierTypeUnknownContentInfoAlgorithmIdentifier () 
		{
			AlgorithmIdentifier ai = new AlgorithmIdentifier ();
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.Unknown, ci, ai);
			DefaultProperties (ep, 2, 0);
		}

		[Test]
		public void ConstructorSubjectIdentifierTypeIssuerAndSerialNumberContentInfo () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.IssuerAndSerialNumber, ci);
			DefaultProperties (ep, 2, 0);
		}

		[Test]
		public void ConstructorSubjectIdentifierTypeSubjectKeyIdentifierContentInfo () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.SubjectKeyIdentifier, ci);
			DefaultProperties (ep, 2, 2);
		}

		[Test]
		public void ConstructorSubjectIdentifierTypeUnknownContentInfo () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.Unknown, ci);
			DefaultProperties (ep, 2, 0);
		}

		[Test]
		[Category ("NotWorking")]
		public void Decode () 
		{
			byte[] encoded = { 0x30, 0x82, 0x01, 0x1C, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x03, 0xA0, 0x82, 0x01, 0x0D, 0x30, 0x82, 0x01, 0x09, 0x02, 0x01, 0x00, 0x31, 0x81, 0xD6, 0x30, 0x81, 0xD3, 0x02, 0x01, 0x00, 0x30, 0x3C, 0x30, 0x28, 0x31, 0x26, 0x30, 0x24, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1D, 0x4D, 0x6F, 0x74, 0x75, 0x73, 0x20, 0x54, 0x65, 0x63, 0x68, 0x6E, 0x6F, 0x6C, 0x6F, 0x67, 0x69, 0x65, 0x73, 0x20, 0x69, 0x6E, 0x63, 0x2E, 0x28, 0x74, 0x65, 0x73, 0x74, 0x29, 0x02, 0x10, 0x91, 0xC4, 0x4B, 0x0D, 0xB7, 0xD8, 0x10, 0x84, 0x42, 0x26, 0x71, 0xB3, 0x97, 0xB5, 0x00, 0x97, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x04, 0x81, 0x80, 0xCA, 0x4B, 0x97, 0x9C, 0xAB, 0x79, 0xC6, 0xDF, 0x6A, 0x27, 0xC7, 0x24, 0xC4, 0x5E, 0x3B, 0x31, 0xAD, 0xBC, 0x25, 0xE6, 0x38, 0x5E, 0x79, 0x26, 0x0E, 0x68, 0x46, 0x1D, 0x21, 0x81, 0x38, 0x92, 0xEC, 0xCB, 0x7C, 0x91, 0xD6, 0x09, 0x38, 0x91, 0xCE, 0x50, 0x5B, 0x70, 0x31, 0xB0, 0x9F, 0xFC, 0xE2, 0xEE, 0x45, 0xBC, 0x4B, 0xF8, 0x9A, 0xD9, 0xEE, 0xE7, 0x4A, 0x3D, 0xCD, 0x8D, 0xFF, 0x10, 0xAB, 0xC8, 0x19, 0x05, 0x54, 0x5E, 0x40, 0x7A, 0xBE, 0x2B, 0xD7, 0x22, 0x97, 0xF3, 0x23, 0xAF, 0x50, 0xF5, 0xEB, 0x43, 0x06, 0xC3, 0xFB, 0x17, 0xCA, 0xBD, 0xAD, 0x28, 0xD8, 0x10, 0x0F, 0x61, 0xCE, 0xF8, 0x25, 0x70, 0xF6, 0xC8, 0x1E, 0x7F, 0x82, 0xE5, 0x94, 0xEB, 0x11, 0xBF, 0xB8, 0x6F, 0xEE, 0x79, 0xCD, 0x63, 0xDD, 0x59, 0x8D, 0x25, 0x0E, 0x78, 0x55, 0xCE, 0x21, 0xBA, 0x13, 0x6B, 0x30, 0x2B, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0x30, 0x14, 0x06, 0x08, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x03, 0x07, 0x04, 0x08, 0x8C, 0x5D, 0xC9, 0x87, 0x88, 0x9C, 0x05, 0x72, 0x80, 0x08, 0x2C, 0xAF, 0x82, 0x91, 0xEC, 0xAD, 0xC5, 0xB5 };
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Decode (encoded);
			// properties
			AssertEquals ("Certificates", 0, ep.Certificates.Count);
			AssertEquals ("ContentEncryptionAlgorithm.KeyLength", 192, ep.ContentEncryptionAlgorithm.KeyLength);
			AssertEquals ("ContentEncryptionAlgorithm.Oid.FriendlyName", tdesName, ep.ContentEncryptionAlgorithm.Oid.FriendlyName);
			AssertEquals ("ContentEncryptionAlgorithm.Oid.Value", tdesOid, ep.ContentEncryptionAlgorithm.Oid.Value);
			AssertEquals ("ContentEncryptionAlgorithm.Parameters", 16, ep.ContentEncryptionAlgorithm.Parameters.Length);
			AssertEquals ("ContentInfo.ContentType.FriendlyName", p7DataName, ep.ContentInfo.ContentType.FriendlyName);
			AssertEquals ("ContentInfo.ContentType.Value", p7DataOid, ep.ContentInfo.ContentType.Value);
			AssertEquals ("ContentInfo.Content", 14, ep.ContentInfo.Content.Length);
			AssertEquals ("RecipientInfos", 1, ep.RecipientInfos.Count);
			RecipientInfo ri = ep.RecipientInfos [0];
			Assert ("RecipientInfos is KeyTransRecipientInfo", (ri is KeyTransRecipientInfo));
			AssertEquals ("UnprotectedAttributes", 0, ep.UnprotectedAttributes.Count);
			AssertEquals ("Version", 0, ep.Version);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void DecodeNull () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Decode (null);
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void EncodeEmpty () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			byte[] encoded = ep.Encode ();
		}

		[Test]
		[Category ("NotWorking")]
		public void Decrypt () 
		{
			byte[] encoded = { 0x30, 0x82, 0x01, 0x1C, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x03, 0xA0, 0x82, 0x01, 0x0D, 0x30, 0x82, 0x01, 0x09, 0x02, 0x01, 0x00, 0x31, 0x81, 0xD6, 0x30, 0x81, 0xD3, 0x02, 0x01, 0x00, 0x30, 0x3C, 0x30, 0x28, 0x31, 0x26, 0x30, 0x24, 0x06, 0x03, 0x55, 0x04, 0x03, 0x13, 0x1D, 0x4D, 0x6F, 0x74, 0x75, 0x73, 0x20, 0x54, 0x65, 0x63, 0x68, 0x6E, 0x6F, 0x6C, 0x6F, 0x67, 0x69, 0x65, 0x73, 0x20, 0x69, 0x6E, 0x63, 0x2E, 0x28, 0x74, 0x65, 0x73, 0x74, 0x29, 0x02, 0x10, 0x91, 0xC4, 0x4B, 0x0D, 0xB7, 0xD8, 0x10, 0x84, 0x42, 0x26, 0x71, 0xB3, 0x97, 0xB5, 0x00, 0x97, 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00, 0x04, 0x81, 0x80, 0xCA, 0x4B, 0x97, 0x9C, 0xAB, 0x79, 0xC6, 0xDF, 0x6A, 0x27, 0xC7, 0x24, 0xC4, 0x5E, 0x3B, 0x31, 0xAD, 0xBC, 0x25, 0xE6, 0x38, 0x5E, 0x79, 0x26, 0x0E, 0x68, 0x46, 0x1D, 0x21, 0x81, 0x38, 0x92, 0xEC, 0xCB, 0x7C, 0x91, 0xD6, 0x09, 0x38, 0x91, 0xCE, 0x50, 0x5B, 0x70, 0x31, 0xB0, 0x9F, 0xFC, 0xE2, 0xEE, 0x45, 0xBC, 0x4B, 0xF8, 0x9A, 0xD9, 0xEE, 0xE7, 0x4A, 0x3D, 0xCD, 0x8D, 0xFF, 0x10, 0xAB, 0xC8, 0x19, 0x05, 0x54, 0x5E, 0x40, 0x7A, 0xBE, 0x2B, 0xD7, 0x22, 0x97, 0xF3, 0x23, 0xAF, 0x50, 0xF5, 0xEB, 0x43, 0x06, 0xC3, 0xFB, 0x17, 0xCA, 0xBD, 0xAD, 0x28, 0xD8, 0x10, 0x0F, 0x61, 0xCE, 0xF8, 0x25, 0x70, 0xF6, 0xC8, 0x1E, 0x7F, 0x82, 0xE5, 0x94, 0xEB, 0x11, 0xBF, 0xB8, 0x6F, 0xEE, 0x79, 0xCD, 0x63, 0xDD, 0x59, 0x8D, 0x25, 0x0E, 0x78, 0x55, 0xCE, 0x21, 0xBA, 0x13, 0x6B, 0x30, 0x2B, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x07, 0x01, 0x30, 0x14, 0x06, 0x08, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x03, 0x07, 0x04, 0x08, 0x8C, 0x5D, 0xC9, 0x87, 0x88, 0x9C, 0x05, 0x72, 0x80, 0x08, 0x2C, 0xAF, 0x82, 0x91, 0xEC, 0xAD, 0xC5, 0xB5 };
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Decode (encoded);

			X509Certificate2 x509 = GetCertificate (true);
			X509Certificate2Collection xc = new X509Certificate2Collection ();
			xc.Add (x509);
			ep.Decrypt (xc);
			// properties
			AssertEquals ("Certificates", 0, ep.Certificates.Count);
			AssertEquals ("ContentEncryptionAlgorithm.KeyLength", 192, ep.ContentEncryptionAlgorithm.KeyLength);
			AssertEquals ("ContentEncryptionAlgorithm.Oid.FriendlyName", tdesName, ep.ContentEncryptionAlgorithm.Oid.FriendlyName);
			AssertEquals ("ContentEncryptionAlgorithm.Oid.Value", tdesOid, ep.ContentEncryptionAlgorithm.Oid.Value);
			AssertEquals ("ContentEncryptionAlgorithm.Parameters", 16, ep.ContentEncryptionAlgorithm.Parameters.Length);
			AssertEquals ("ContentInfo.ContentType.FriendlyName", p7DataName, ep.ContentInfo.ContentType.FriendlyName);
			AssertEquals ("ContentInfo.ContentType.Value", p7DataOid, ep.ContentInfo.ContentType.Value);
			AssertEquals ("ContentInfo.Content", "05-00", BitConverter.ToString (ep.ContentInfo.Content));
			AssertEquals ("RecipientInfos", 1, ep.RecipientInfos.Count);
			AssertEquals ("UnprotectedAttributes", 0, ep.UnprotectedAttributes.Count);
			AssertEquals ("Version", 0, ep.Version);
		}

		[Test]
		[ExpectedException (typeof (InvalidOperationException))]
		public void DecryptEmpty () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Decrypt ();
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void DecryptRecipientInfoNull () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			RecipientInfo ri = null; // do not confuse compiler
			ep.Decrypt (ri);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void DecryptX509CertificateExCollectionNull () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			X509Certificate2Collection xec = null; // do not confuse compiler
			ep.Decrypt (xec);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void DecryptRecipientInfoX509CertificateExCollectionNull () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			X509Certificate2Collection xec = new X509Certificate2Collection ();
			ep.Decrypt (null, xec);
		}

/*		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void DecryptX509CertificateExCollectionNull () 
		{
			EnvelopedPkcs7 ep = new EnvelopedPkcs7 ();
			RecipientInfo ri = 
			ep.Decrypt (ri, null);
		}*/

		private void RoundTrip (byte[] encoded) 
		{
			X509Certificate2Collection xc = new X509Certificate2Collection ();
			xc.Add (GetCertificate (true));
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Decode (encoded);
			ep.Decrypt (xc);
			AssertEquals ("ContentInfo.Content", "05-00", BitConverter.ToString (ep.ContentInfo.Content));
		}

		[Test]
		[Category ("NotWorking")]
		public void EncryptCmsRecipientIssuerAndSerialNumber () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.IssuerAndSerialNumber, ci);

			X509Certificate2 x509 = GetCertificate (false);
			CmsRecipient p7r = new CmsRecipient (SubjectIdentifierType.IssuerAndSerialNumber, x509);
			ep.Encrypt (p7r);
			byte[] encoded = ep.Encode ();
#if DEBUG
			FileStream fs = File.OpenWrite ("EncryptCmsRecipientIssuerAndSerialNumber.der");
			fs.Write (encoded, 0, encoded.Length);
			fs.Close ();
#endif
			RoundTrip (encoded);
		}

		[Test]
		[Category ("NotWorking")]
		public void EncryptCmsRecipientSubjectKeyIdentifier () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.IssuerAndSerialNumber, ci);

			X509Certificate2 x509 = GetCertificate (false);
			CmsRecipient p7r = new CmsRecipient (SubjectIdentifierType.SubjectKeyIdentifier, x509);
			ep.Encrypt (p7r);
			byte[] encoded = ep.Encode ();
#if DEBUG			
			FileStream fs = File.OpenWrite ("EncryptCmsRecipientSubjectKeyIdentifier.der");
			fs.Write (encoded, 0, encoded.Length);
			fs.Close ();
#endif
			RoundTrip (encoded);
		}

		[Test]
		[Category ("NotWorking")]
		public void EncryptCmsRecipientUnknown () 
		{
			ContentInfo ci = new ContentInfo (asnNull);
			EnvelopedCms ep = new EnvelopedCms (SubjectIdentifierType.IssuerAndSerialNumber, ci);

			X509Certificate2 x509 = GetCertificate (false);
			CmsRecipient p7r = new CmsRecipient (SubjectIdentifierType.Unknown, x509);
			ep.Encrypt (p7r);
			byte[] encoded = ep.Encode ();
#if DEBUG			
			FileStream fs = File.OpenWrite ("EncryptCmsRecipientUnknown.der");
			fs.Write (encoded, 0, encoded.Length);
			fs.Close ();
#endif
			RoundTrip (encoded);
		}

		[Test]
		[Category ("NotWorking")]
		[ExpectedException (typeof (CryptographicException))]
		public void EncryptEmpty () 
		{
			EnvelopedCms ep = new EnvelopedCms ();
			ep.Encrypt ();
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void EnvelopedCmsRecipientNull ()
		{
			EnvelopedCms ep = new EnvelopedCms ();
			CmsRecipient p7r = null; // do not confuse compiler
			ep.Encrypt (p7r);
		}

		[Test]
		[ExpectedException (typeof (ArgumentNullException))]
		public void EnvelopedCmsRecipientCollectionNull ()
		{
			EnvelopedCms ep = new EnvelopedCms ();
			CmsRecipientCollection p7rc = null; // do not confuse compiler
			ep.Encrypt (p7rc);
		}
	}
}

#endif
