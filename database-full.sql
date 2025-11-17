--
-- PostgreSQL database dump
--

\restrict gKQctKH7s38i6Nd2B5l3gD5RolxDwBGfcXJ13rWxM0hVQhAu7zxxN0UTMuBo4Hw

-- Dumped from database version 14.19 (Homebrew)
-- Dumped by pg_dump version 14.19 (Homebrew)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Category; Type: TABLE; Schema: public; Owner: jasonluong
--

CREATE TABLE public."Category" (
    "CategoryId" integer NOT NULL,
    "CategoryName" character varying(255) NOT NULL
);


ALTER TABLE public."Category" OWNER TO jasonluong;

--
-- Name: Category_CategoryId_seq; Type: SEQUENCE; Schema: public; Owner: jasonluong
--

CREATE SEQUENCE public."Category_CategoryId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Category_CategoryId_seq" OWNER TO jasonluong;

--
-- Name: Category_CategoryId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jasonluong
--

ALTER SEQUENCE public."Category_CategoryId_seq" OWNED BY public."Category"."CategoryId";


--
-- Name: Customer; Type: TABLE; Schema: public; Owner: jasonluong
--

CREATE TABLE public."Customer" (
    "CustomerId" integer NOT NULL,
    "CustomerName" character varying(255) NOT NULL,
    "CustomerPhone" character varying(15) NOT NULL,
    "CustomerEmail" character varying(255),
    "CustomerAddress" character varying(255),
    "Username" character varying(255) NOT NULL
);


ALTER TABLE public."Customer" OWNER TO jasonluong;

--
-- Name: Customer_CustomerId_seq; Type: SEQUENCE; Schema: public; Owner: jasonluong
--

CREATE SEQUENCE public."Customer_CustomerId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Customer_CustomerId_seq" OWNER TO jasonluong;

--
-- Name: Customer_CustomerId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jasonluong
--

ALTER SEQUENCE public."Customer_CustomerId_seq" OWNED BY public."Customer"."CustomerId";


--
-- Name: Order; Type: TABLE; Schema: public; Owner: jasonluong
--

CREATE TABLE public."Order" (
    "OrderId" integer NOT NULL,
    "CustomerId" integer NOT NULL,
    "OrderDate" timestamp without time zone NOT NULL,
    "TotalAmount" numeric(18,2) NOT NULL,
    "PaymentStatus" character varying(50),
    "AddressDelivery" character varying(255) NOT NULL
);


ALTER TABLE public."Order" OWNER TO jasonluong;

--
-- Name: OrderDetail; Type: TABLE; Schema: public; Owner: jasonluong
--

CREATE TABLE public."OrderDetail" (
    "Id" integer NOT NULL,
    "ProductId" integer NOT NULL,
    "OrderId" integer NOT NULL,
    "Quantity" integer NOT NULL,
    "UnitPrice" numeric(18,2) NOT NULL
);


ALTER TABLE public."OrderDetail" OWNER TO jasonluong;

--
-- Name: OrderDetail_Id_seq; Type: SEQUENCE; Schema: public; Owner: jasonluong
--

CREATE SEQUENCE public."OrderDetail_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."OrderDetail_Id_seq" OWNER TO jasonluong;

--
-- Name: OrderDetail_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jasonluong
--

ALTER SEQUENCE public."OrderDetail_Id_seq" OWNED BY public."OrderDetail"."Id";


--
-- Name: Order_OrderId_seq; Type: SEQUENCE; Schema: public; Owner: jasonluong
--

CREATE SEQUENCE public."Order_OrderId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Order_OrderId_seq" OWNER TO jasonluong;

--
-- Name: Order_OrderId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jasonluong
--

ALTER SEQUENCE public."Order_OrderId_seq" OWNED BY public."Order"."OrderId";


--
-- Name: Product; Type: TABLE; Schema: public; Owner: jasonluong
--

CREATE TABLE public."Product" (
    "ProductId" integer NOT NULL,
    "CategoryId" integer NOT NULL,
    "ProductName" character varying(255) NOT NULL,
    "ProductDescription" text NOT NULL,
    "ProductPrice" numeric(18,2) NOT NULL,
    "ProductImage" character varying(255)
);


ALTER TABLE public."Product" OWNER TO jasonluong;

--
-- Name: Product_ProductId_seq; Type: SEQUENCE; Schema: public; Owner: jasonluong
--

CREATE SEQUENCE public."Product_ProductId_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Product_ProductId_seq" OWNER TO jasonluong;

--
-- Name: Product_ProductId_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: jasonluong
--

ALTER SEQUENCE public."Product_ProductId_seq" OWNED BY public."Product"."ProductId";


--
-- Name: User; Type: TABLE; Schema: public; Owner: jasonluong
--

CREATE TABLE public."User" (
    "Username" character varying(255) NOT NULL,
    "Password" character varying(50) NOT NULL,
    "UserRole" character(1) NOT NULL
);


ALTER TABLE public."User" OWNER TO jasonluong;

--
-- Name: Category CategoryId; Type: DEFAULT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Category" ALTER COLUMN "CategoryId" SET DEFAULT nextval('public."Category_CategoryId_seq"'::regclass);


--
-- Name: Customer CustomerId; Type: DEFAULT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Customer" ALTER COLUMN "CustomerId" SET DEFAULT nextval('public."Customer_CustomerId_seq"'::regclass);


--
-- Name: Order OrderId; Type: DEFAULT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Order" ALTER COLUMN "OrderId" SET DEFAULT nextval('public."Order_OrderId_seq"'::regclass);


--
-- Name: OrderDetail Id; Type: DEFAULT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."OrderDetail" ALTER COLUMN "Id" SET DEFAULT nextval('public."OrderDetail_Id_seq"'::regclass);


--
-- Name: Product ProductId; Type: DEFAULT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Product" ALTER COLUMN "ProductId" SET DEFAULT nextval('public."Product_ProductId_seq"'::regclass);


--
-- Data for Name: Category; Type: TABLE DATA; Schema: public; Owner: jasonluong
--

COPY public."Category" ("CategoryId", "CategoryName") FROM stdin;
7	sách khoa học
8	truyện tranh
9	tiểu thuyết
10	kinh tế
\.


--
-- Data for Name: Customer; Type: TABLE DATA; Schema: public; Owner: jasonluong
--

COPY public."Customer" ("CustomerId", "CustomerName", "CustomerPhone", "CustomerEmail", "CustomerAddress", "Username") FROM stdin;
1	Quân	0901716543	jasonluong1808@gmail.com	\N	doris18
2	Quân	0901716543	jasonluong1808@gmail.com	123 phường vĩnh hội tphcm\r\n	doris18
3	Quân	0901716543	jasonluong1808@gmail.com	123 phường vĩnh hội tphcm\r\n	doris18
4	Quân	0901716543	jasonluong1808@gmail.com	123 p.saigon tphcm	doris18
5	Quân	0901716543	jasonluong1808@gmail.com	123 p.sài gòn tphcm	doris18
7	Quân	0901716543	jasonluong1808@gmail.com	123 p.sai gòn tphcm\r\n	doris18
8	Quân	0901716543	jasonluong1808@gmail.com	123 p.sài gòn tphcm\r\n	doris18
\.


--
-- Data for Name: Order; Type: TABLE DATA; Schema: public; Owner: jasonluong
--

COPY public."Order" ("OrderId", "CustomerId", "OrderDate", "TotalAmount", "PaymentStatus", "AddressDelivery") FROM stdin;
1	2	2025-11-15 00:00:00	182000.00	Pending	123 phường vĩnh hội tphcm\r\n
2	3	2025-11-15 00:00:00	146000.00	Pending	123 phường vĩnh hội tphcm\r\n
3	4	2025-11-15 00:00:00	109000.00	Pending	123 p.saigon tphcm
4	5	2025-11-16 00:00:00	100000.00	Pending	123 p.sài gòn tphcm
5	7	2025-11-16 00:00:00	210000.00	Pending	123 p.sai gòn tphcm\r\n
6	8	2025-11-17 00:00:00	300000.00	Pending	123 p.sài gòn tphcm\r\n
\.


--
-- Data for Name: OrderDetail; Type: TABLE DATA; Schema: public; Owner: jasonluong
--

COPY public."OrderDetail" ("Id", "ProductId", "OrderId", "Quantity", "UnitPrice") FROM stdin;
1	3	1	1	73000.00
2	2	1	1	109000.00
3	3	2	2	73000.00
4	2	3	1	109000.00
5	4	4	1	100000.00
6	3	5	1	73000.00
7	1	5	1	137000.00
8	4	6	3	100000.00
\.


--
-- Data for Name: Product; Type: TABLE DATA; Schema: public; Owner: jasonluong
--

COPY public."Product" ("ProductId", "CategoryId", "ProductName", "ProductDescription", "ProductPrice", "ProductImage") FROM stdin;
2	7	30 giây khoa học	30 Giây Khoa Học là một bộ sách tuyệt vời dành cho những người muốn khám phá và hiểu biết về các khía cạnh khác nhau của khoa học và nghệ thuật.	109000.00	https://product.hstatic.net/200000343865/product/30-giay-khoa-hoc_hoc-thuyet_6c416c01c2fb49cab5688b8eb03589ad_master.jpg
3	9	Những cuộc phiêu lưu của Sherlock Holmes	Sherlock Holmes là một nhân vật thám tử hư cấu, xuất hiện lần đầu trong tác phẩm của nhà văn Arthur Conan Doyle xuất bản năm 1887. Nhân vật là một thám tử tư ở London, nổi tiếng nhờ trí thông minh, khả năng suy diễn logic và quan sát tinh tường trong khi phá những vụ án mà cảnh sát phải bó tay. Nhiều người cho rằng Sherlock Holmes là nhân vật thám tử hư cấu nổi tiếng nhất trong lịch sử NXB Văn Học và là một trong những nhân vật NXB Văn Học được biết đến nhiều nhất trên toàn thế giớ	73000.00	https://bizweb.dktcdn.net/thumb/1024x1024/100/370/339/products/img-8485.jpg?v=1691467038237
1	8	thám tử lừng danh Conan	Mei Tantei Konan - Detective Conan (Case Closed) 107 [Regular Edition]	137000.00	https://cdn1.fahasa.com/media/catalog/product/9/7/9784098540792.jpg
4	9	Sherlock Holmes - Công Việc Sau Cùng Của Holmes	Sherlock Holmes\r\n\r\nĐối với những độc giả yêu thích dòng trinh thám nói riêng cũng như những người yêu sách trên toàn thế giới nói chung thì không phải nói nhiều về sức hút của hai cái tên: Conan Doyle và " đứa con tinh thần" cả đời của ông Sherlock Holmes.\r\n\r\nSherlock Holmes từ lâu đã trở thành nguồn cảm hứng cho hàng trăm, hàng ngàn các tác phẩm ở nhiều loại hình nghệ thuật khác nhau: từ âm nhạc, ca kịch đến điện ảnh.	100000.00	https://cdn1.fahasa.com/media/catalog/product/9/7/9786043720136.jpg
5	10	 Biến Mọi Thứ Thành Tiền - Make Money	Cuốn sách chỉ ra cho bạn phương thức, công cụ phù hợp đạt được cơ hội chiến thắng là hiểu chính bản thân mình.\r\n\r\n“Biến mọi thứ thành tiền” gồm 3 phần lớn:\r\n\r\nPhần 1: Khát vọng biến mọi thứ thành tiền\r\n\r\nPhần 2: Biến mọi thứ thành tiền\r\n\r\nPhần 3: Sáng tạo dòng tiền	84000.00	https://cdn1.fahasa.com/media/catalog/product/9/7/9786043654370.jpg
6	10	Báo Cáo Tài Chính Dưới Góc Nhìn Của Warren Buffett 	“Benjamin Graham, “ông chủ” của Phố Wall và là thầy của Warren, đã ứng dụng các kỹ thuật phân tích trái phiếu sơ khai vào việc phân tích các cổ phiếu thông dụng. Nhưng Graham không bao giờ phân biệt giữa một công ty đang có lợi thế cạnh tranh dài hạn so với các đối thủ cạnh tranh và một công ty không có lợi thế đó. Ông chỉ quan tâm liệu công ty có đủ khả năng tạo lợi nhuận để giúp nó thoát khỏi khó khan về kinh tế đã đẩy giá cổ phiếu giảm lien tục hay không. Nếu giá không biến động sau hai năm, ông luền bán cổ phiếu đi. Không phải Graham đã lỡ chuyến tàu, mà chỉ là ông ấy không lên đúng chuyến tàu giúp ông trở thành người giàu nhất thế giới như Warren.	98000.00	https://cdn1.fahasa.com/media/catalog/product/9/7/9786043984057.jpg
7	10	Tài Chính Cá Nhân Dành Cho Người Việt Nam 	Cuốn sách “Tài chính cá nhân dành cho người Việt Nam” và bộ bài giảng trực tuyến tặng kèm, gồm tất cả những nội dung về tài chính cá nhân.\r\n\r\nKiếm tiền: Khi còn thể làm việc, chúng ta cần kiếm tiền, tạo ra thu nhập với "công suất" lớn nhất.\r\n\r\nTiết kiệm tiền, sử dụng tiền: Chúng ta phải quản lý tiền hiệu quá, sử dụng tiền khôn ngoan. Đặc biệt, chúng ta phải tiết kiệm trước khi sử dụng.\r\n\r\nBảo vệ tiền: Chúng ta phải biết bảo vệ tiền sự mất mát của tiền trước những rủi ro.\r\n\r\nĐầu tư tiền: Tiền phải sinh ra tiền. Chúng ta phải đầu tư để tiền tăng trưởng và đạt được mục tiêu tài chính cá nhân.	150000.00	https://cdn1.fahasa.com/media/catalog/product/9/7/9786043905113.jpg
8	7	 30 Giây Khoa Học Dữ Liệu	30 Giây Khoa học dữ liệu bao gồm các nguyên tắc thống kê cơ bản thúc đẩy các thuật toán và cách thức dữ liệu ảnh hưởng đến chúng ta trong mọi lĩnh vực khoa học, xã hội, kinh doanh, giải trí - cùng với các vấn đề đạo đức và lời hứa về một thế giới tốt đẹp hơn trong tương lai. Mỗi đoạn văn 30 giây trình bày chi tiết một khía cạnh của khoa học dữ liệu chỉ trong khoảng 300 từ và một hình minh họa, miêu tả làm thế nào để thu thập các loại dữ liệu khác nhau và sử dụng các chương trình máy tính để tìm ra các qui luật mà mắt người không thể phát hiện ra, góp phần làm thay đổi thế giới. Tìm hiểu những ý tưởng và tiểu sử của những vĩ nhân đằng sau chúng, 30 giây Khoa học dữ liệu là cách nhanh nhất để khám phá cách dữ liệu ảnh hưởng mạnh mẽ đến các vấn đề lớn như biến đổi khí hậu, chăm sóc sức khỏe và cả những chi tiết nhỏ trong cuộc sống hàng ngày của chúng ta.	139000.00	https://cdn1.fahasa.com/media/catalog/product/3/0/30-giay-khoa-hoc_khoa-hoc-du-lieu.jpg
\.


--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: jasonluong
--

COPY public."User" ("Username", "Password", "UserRole") FROM stdin;
admin	21232F297A57A5A743894A0E4A801FC3	A
doris18	7E310C14E996F89A9F561AB88FDE7688	C
\.


--
-- Name: Category_CategoryId_seq; Type: SEQUENCE SET; Schema: public; Owner: jasonluong
--

SELECT pg_catalog.setval('public."Category_CategoryId_seq"', 10, true);


--
-- Name: Customer_CustomerId_seq; Type: SEQUENCE SET; Schema: public; Owner: jasonluong
--

SELECT pg_catalog.setval('public."Customer_CustomerId_seq"', 8, true);


--
-- Name: OrderDetail_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: jasonluong
--

SELECT pg_catalog.setval('public."OrderDetail_Id_seq"', 8, true);


--
-- Name: Order_OrderId_seq; Type: SEQUENCE SET; Schema: public; Owner: jasonluong
--

SELECT pg_catalog.setval('public."Order_OrderId_seq"', 6, true);


--
-- Name: Product_ProductId_seq; Type: SEQUENCE SET; Schema: public; Owner: jasonluong
--

SELECT pg_catalog.setval('public."Product_ProductId_seq"', 8, true);


--
-- Name: Category Category_pkey; Type: CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Category"
    ADD CONSTRAINT "Category_pkey" PRIMARY KEY ("CategoryId");


--
-- Name: Customer Customer_pkey; Type: CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Customer"
    ADD CONSTRAINT "Customer_pkey" PRIMARY KEY ("CustomerId");


--
-- Name: OrderDetail OrderDetail_pkey; Type: CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."OrderDetail"
    ADD CONSTRAINT "OrderDetail_pkey" PRIMARY KEY ("Id");


--
-- Name: Order Order_pkey; Type: CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "Order_pkey" PRIMARY KEY ("OrderId");


--
-- Name: Product Product_pkey; Type: CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT "Product_pkey" PRIMARY KEY ("ProductId");


--
-- Name: User User_pkey; Type: CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "User_pkey" PRIMARY KEY ("Username");


--
-- Name: Customer Customer_Username_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Customer"
    ADD CONSTRAINT "Customer_Username_fkey" FOREIGN KEY ("Username") REFERENCES public."User"("Username");


--
-- Name: OrderDetail OrderDetail_OrderId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."OrderDetail"
    ADD CONSTRAINT "OrderDetail_OrderId_fkey" FOREIGN KEY ("OrderId") REFERENCES public."Order"("OrderId");


--
-- Name: OrderDetail OrderDetail_ProductId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."OrderDetail"
    ADD CONSTRAINT "OrderDetail_ProductId_fkey" FOREIGN KEY ("ProductId") REFERENCES public."Product"("ProductId");


--
-- Name: Order Order_CustomerId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Order"
    ADD CONSTRAINT "Order_CustomerId_fkey" FOREIGN KEY ("CustomerId") REFERENCES public."Customer"("CustomerId");


--
-- Name: Product Product_CategoryId_fkey; Type: FK CONSTRAINT; Schema: public; Owner: jasonluong
--

ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT "Product_CategoryId_fkey" FOREIGN KEY ("CategoryId") REFERENCES public."Category"("CategoryId");


--
-- PostgreSQL database dump complete
--

\unrestrict gKQctKH7s38i6Nd2B5l3gD5RolxDwBGfcXJ13rWxM0hVQhAu7zxxN0UTMuBo4Hw

