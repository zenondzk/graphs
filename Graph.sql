-- Database: graph

-- DROP DATABASE graph;

CREATE DATABASE graph
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'English_United States.1252'
    LC_CTYPE = 'English_United States.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;
	
	-- Table: public."Graph"

-- DROP TABLE public."Graph";

CREATE TABLE public."Graph"
(
    "Id" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT graph_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Graph"
    OWNER to postgres;
	
	-- Table: public."Node"

-- DROP TABLE public."Node";

CREATE TABLE public."Node"
(
    "Id" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "GraphId" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT node_pkey PRIMARY KEY ("Id"),
    CONSTRAINT graph_node_graph_id_fkey FOREIGN KEY ("GraphId")
        REFERENCES public."Graph" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Node"
    OWNER to postgres;
	
	-- Table: public."Edge"

-- DROP TABLE public."Edge";

CREATE TABLE public."Edge"
(
    "Id" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "To" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "From" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "Cost" numeric(10,2),
    "GraphId" character varying(10) COLLATE pg_catalog."default",
    CONSTRAINT edge_pkey PRIMARY KEY ("Id"),
    CONSTRAINT "FK_From_Node" FOREIGN KEY ("From")
        REFERENCES public."Node" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_GraphId" FOREIGN KEY ("GraphId")
        REFERENCES public."Graph" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "FK_To_Node" FOREIGN KEY ("To")
        REFERENCES public."Node" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public."Edge"
    OWNER to postgres;