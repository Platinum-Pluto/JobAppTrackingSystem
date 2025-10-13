CREATE DATABASE job_tracking_app;

CREATE TABLE jobs(
    job_id INT PRIMARY KEY,
    company_name VARCHAR(255),
    position VARCHAR(255)
);

CREATE TABLE users(
    user_id INT PRIMARY KEY,
    name VARCHAR(255),
    email VARCHAR(255) UNIQUE,
    password VARCHAR(255),
    country VARCHAR(255),
    district VARCHAR(255)
);

CREATE TABLE admin(
    admin_id INT PRIMARY KEY,
    name VARCHAR(255),
    email VARCHAR(255),
    password VARCHAR(255)
);


CREATE TABLE applications(
    application_id INT PRIMARY KEY,
    user_id INT,
    job_id INT,
    status ENUM('Applied', 'Accepted', 'Rejected'),
    applied_date timestamp,
    priority ENUM('Low', 'Medium', 'High'),
    FOREIGN KEY (user_id) REFERENCES users(user_id),
    FOREIGN KEY (job_id) REFERENCES jobs(job_id)
);



INSERT INTO users (user_id, name, email, password, country, district)VALUES
(1, 'Alice', 'alice@example.com', 'djwsdjijd', 'Panem', 'District 9'),
(2, 'Bob', 'bob@example.com', 'dsaidijij', 'Panem', 'District 1'),
(3, 'Marco', 'marco@example.com', 'swi9oiojdc', 'Panem', 'District 7');


INSERT INTO admin (admin_id, name, email, password)VALUES
(1, 'Jack', 'jack@example.com', 'ksodkso'),
(2, 'John', 'john@example.com', 'djsijdi'),
(3, 'Jill', 'jill@example.com', 'dsdsdauj');


INSERT INTO jobs (job_id, company_name, position) VALUES
(101, 'TechCorp', 'Software Engineer'),
(102, 'DataWorks', 'Data Analyst'),
(103, 'Meta', 'Data Engineer');


INSERT INTO applications (application_id, user_id, job_id, status, applied_date, priority) VALUES
(1001, 1, 101, 'Applied', '2025-10-01', 'Medium'),
(1002, 1, 102, 'Rejected', '2025-11-01', 'High'),
(1003, 1, 101, 'Accepted', '2025-10-01', 'Low'),
(1004, 2, 101, 'Accepted', '2025-11-01', 'High'),
(1005, 3, 103, 'Applied', '2025-12-01', 'Low');
