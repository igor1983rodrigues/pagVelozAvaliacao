create table tbl_empresa (
	id_empresa serial not null,
	nome_empresa varchar(255) not null,
	uf_empresa varchar(2) not null,
	cnpj_empresa varchar(14) not null,
	constraint id_tbl_empresa_pk primary key (id_empresa)	
);

create table tbl_fornecedor (
id_fornecedor serial not null,
id_empresa integer not null,
nr_doc_fornecedor varchar(14) not null,
dt_cad_fornecedor timestamp not null default now(),
constraint tbl_fornecedor_pk primary key (id_fornecedor),
constraint tbl_empresa_fk foreign key (id_empresa)
	references tbl_empresa (id_empresa) match simple
	on update cascade
	on delete cascade
);

create table tbl_telefone (
	id_telefone serial not null,
	id_fornecedor integer not null,
	numero_telefone varchar(11) not null,
	constraint tbl_telefone_pk primary key (id_telefone),
	constraint tbl_fornecedor_fk foreign key (id_fornecedor)
		references tbl_fornecedor (id_fornecedor) match simple
		on update cascade
		on delete cascade
);



